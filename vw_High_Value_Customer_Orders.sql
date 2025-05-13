Alter view vw_High_Value_Customer_Orders as
SELECT sh.SalesOrderID,  sh.CustomerID , p.FirstName + ' '+ p.LastName [Customer],sh.TotalDue TotalAmount, sd.ProductID ProductId,
	  prod.Name ProductName, prod.ProductNumber ProductNumber,sd.OrderQty Qty,
     sd.UnitPrice UnitPrice,( sd.OrderQty * sd.UnitPrice) LineTotoal
FROM Sales.SalesOrderHeader sh inner join  Sales.SalesOrderDetail sd on sd.[SalesOrderID] = sh.[SalesOrderID]
										inner join  Production.Product prod on prod.ProductID = sd.ProductID
										inner join  Sales.Customer cust on cust.CustomerID = sh.CustomerID
										inner join  Person.Person p on p.BusinessEntityID = cust.PersonID									 
										group by sh.SalesOrderID, sh.CustomerID ,  p.FirstName ,p.LastName ,sh.TotalDue, sd.ProductID, sd.OrderQty, sd.UnitPrice , prod.Name, prod.ProductNumber
										having  sh.TotalDue  > 5000

