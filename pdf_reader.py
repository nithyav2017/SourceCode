import sys
print(sys.executable)
import fitz

def extract_text_from_pdf(pdf_path):

    pdf_document = fitz.open(pdf_path)

    text =""

    for page_num in range(len(pdf_document)):
        page = pdf_document.load_page(page_num)
        text += page.get_text()

    return text

    if __name__ == "__main__":
        pdf_file_path = r"C:\Users\djaya\Documents\Nithya\Python\Application-Form-PDF.pdf"
        text = extract_text_from_pdf(pdf_file_path)
        print(text)
