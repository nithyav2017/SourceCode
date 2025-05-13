SELECT P.[NAME] , py.Quantity , pr.FirstName + ' '+pr.LastName ,  50 - py.Quantity 'Replenishment Quantity' 
 FROM 
        Production.Product p
    JOIN
        Production.ProductInventory py
        ON p.ProductID = py.ProductID
    JOIN        
        [Purchasing].[ProductVendor] pv
		ON p.ProductID = pv.ProductID
	JOIN
		[Person].[Person] pr
		ON pr.BusinessEntityID = pv.BusinessEntityID
	JOIN [Production].[ProductListPriceHistory] ph
	ON ph.[ProductID] = p.ProductID   
	WHERE py.Quantity < 20 
	order by py.Quantity asc

