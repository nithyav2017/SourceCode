import fitz  # PyMuPDF
import os
import pyodbc
import time
from datetime import date
import re


conn = pyodbc.connect(
    'DRIVER={ODBC Driver 17 for SQL Server};'
    'SERVER=localhost;'
    'DATABASE=employee;'
    'UID=sa;'
    'PWD=Learning@12'
)

# ✅ Step 2: Create cursor
cursor = conn.cursor()

# ✅ Step 3: Extract PDF Text ======
def extract_text_and_signature(pdf_path, signature_save_path):
    pdf_document = fitz.open(pdf_path)
    text = ""
    data = {}

    for page_num in range(len(pdf_document)):
        page = pdf_document.load_page(page_num)

        # Extract all text from page
        text += page.get_text()
        #Check if DOB and Email already exist
        # Use regex to extract DOB and email
        #dob_pattern = r"\b\d{4}-\d{2}-\d{2}\b"  # Matches YYYY-MM-DD format
        dob_pattern = r"\b\d{2}/\d{2}/\d{4}\b"
        email_pattern = r"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}"

        dob_match = re.findall(dob_pattern,text)
        email_match = re.findall(email_pattern, text)
        if dob_match and email_match:
            if(record_is_exists(cursor, dob_match[1], email_match[0])):
                  print(f"The record { email_match[0]} already exists. Skipping insert.")
            else:
                # Check for "Applicant's Signature" label location
                signature_rects = page.search_for("Applicant's Signature")

                # If label found, attempt to extract nearby image
                if signature_rects:
                    signature_label_rect = signature_rects[0]
                    # Define the capture area (adjust as needed)
                    # Capture 200px to the right and 60px down
                    signature_area = fitz.Rect(
                        signature_label_rect.x1 + 5,
                        signature_label_rect.y0,
                        signature_label_rect.x1 + 200,
                        signature_label_rect.y1 + 5
                    )
                    # Render image from that region
                    pix = page.get_pixmap(clip=signature_area)
                    timestamp = int(time.time())
                    # Create folder if it doesn't exist
                    os.makedirs(signature_save_path, exist_ok=True)
                    image_path = os.path.join(signature_save_path, f"signature_{page_num }_{timestamp}.png")
                    pix.save(image_path)
                    print(f"✅ Saved signature snapshot: {image_path}")
                    #break  # stop after finding first signature
        
                # Split and extract form data
                lines = text.strip().split('\n')

                label_count = 17  # known number of labels in the form
                labels = [line.rstrip(':').strip() for line in lines[:label_count]]
                values = [line.strip() for line in lines[label_count+1:]]

                while len(values) < len(labels):
                   values.append(None)

                data = dict(zip(labels, values))
                    
                #===✅ Step 3: Save Record====
                try:
                   cursor.execute("""
                            INSERT INTO PersonalInformation (Name, DateOfBirth, Address, PhoneNumber, Email)
                            OUTPUT INSERTED.Id
                            VALUES (?, ?, ?, ?, ?)
                        """, (
                            data.get("Name"),
                            data.get("Date of Birth"),
                            data.get("Address"),
                            data.get("Phone Number"),
                            data.get("Email")
                        ))
                        #conn.commit()
                   personal_id = cursor.fetchone()[0]

                      
                   with open(image_path, "rb") as f:
                            image_data = f.read()
                   cursor.execute(""" 
                            INSERT INTO Signature (PersonalId ,ApplicantSignature,SubmissionDate )
                            VALUES(?,?,?)
                        """,(
                            personal_id,
                            image_data,
                            date.today()
                        ))

                   cursor.execute("""
                            INSERT INTO [PreviousEmployment] 
                                    ([PersonalId]
                                   ,[CompanyName]
                                   ,[Role]
                                   ,[StartEndDates]
                                   ,[Responsibilities])
                            VALUES (?, ?, ?, ?, ?)
                        """, (
                            personal_id,
                            data.get("Company Name"),
                            data.get("Role"),
                            data.get("Start and End Dates"),
                            data.get("Responsibilities")
                        ))
                   cursor.execute("""
                            INSERT INTO [EducationalBackground] 
                                    ([PersonalId]
                                   ,[HighestEducation]
                                   ,[Institution]
                                   ,[GraduationYear])                                   
                            VALUES (?, ?, ?, ?)
                        """, (
                            personal_id,
                            data.get("Highest Level of Education"),
                            data.get("Institution"),
                            data.get("Graduation Year")
                        ))
                   conn.commit()
                   print (f"Personal id:{personal_id}")
                except Exception as e:
                        print("DB Error:", e)

                print("\n✅ Extracted Text Data:")
                for label, value in data.items():
                        print(f"{label}: {value}")
                return data
        else:
            print("Error: No DOB or Email found in the text")

def record_is_exists(cursor, DOB, Email):
    cursor.execute("select 1 from PersonalInformation  where DateOfBirth = ? and Email = ?", (DOB, Email))
    return cursor.fetchone() is not None

# === USAGE ===
pdf_path = r"C:\Users\nithy\OneDrive\Documentos\Nithya\Python\Application_Form.pdf"
signature_save_folder = r"C:\Users\nithy\OneDrive\Documentos\Nithya\Python\signature_images"
data = extract_text_and_signature(pdf_path, signature_save_folder)
