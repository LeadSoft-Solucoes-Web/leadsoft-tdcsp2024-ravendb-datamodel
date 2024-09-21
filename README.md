# leadsoft-tdcsp2024-ravendb-datamodel
Repositório exemplo do Workshop TDC SP 2014: **NoSQL: Um pequeno passo na modelagem, um grande salto na eficiência**
- Trilha NoSQL no dia 20 de setembro de 2024 no The Developer Conference São Paulo
- Link no YouTube:
  - (Em breve) 

## Conteúdo da apresentação

- Em um contexto de banco de dados SQL
- A visualização tabelar é bem familiar...
- Em um contexto de banco de dados SQL
- Mas então, como simplificar?
- A vida é uma aventura! Seu banco de dados não deveria ser...
- A simplicidade de consultas usando LINQ
- Não precisa de Join?
- Entenda as rotas em cada caso
- E meus dados, como ficam?
- Quer saber mais como usar RavenDB?
- Sobre o Palestrante
  - https://www.leadsoft.inf.br/crew/lucas_tavares/contact
- A LeadSoft
  - Clientes que decolaram
  - Manifesto
  - Mais sobre nós
 
## Scripts
### SQL

~~~~sql
SELECT 
  ord.Order_ID AS 'NumeroPedido', 
  ord.OrderDate AS 'DiaDaCompra', 
  (
    select 
      count(op.Order_Product_ID) 
    from 
      OrderProduct op 
    where 
      op.Order_ID = ord.Order_ID
  ) AS 'QtdeItens', 
  (
    select 
      sum(op.Price * ap.Amount) 
    from 
      OrderProduct op 
    where 
      op.Order_ID = ord.Order_ID
  ) AS 'TotalItens', 
  ord.Discount AS 'Desconto', 
  ((
      select 
        sum(op.Price * ap.Amount) 
      from 
        OrderProduct op 
      where 
        op.Order_ID =
    ) - ord.Discount) AS 'TotalDaVenda', 
  cus.Name AS 'EntregaPara', 
  del.Type AS 'TipoEntrega', 
  del.Status AS 'StatusEntrega', 
  sta.Name AS 'EntragaEm' 
FROM 
  Order ord 
  INNER JOIN Customer cus ON cus.IsActive = 1 
  and cus.Customer_ID = ord.CUSTOMER_ID 
  LEFT JOIN Delivery del ON del.Order_ID = ord.Order_ID 
  LEFT JOIN Customer_Address cua ON cua.Customer_Address_ID = del.Customer_Address_ID 
  and (
    cua.State_ID IN (
      select 
        s.STATE_ID 
      from 
        State s 
      where 
        s.Initials in ('SP', 'MG', 'RJ', 'ES')
    )
  ) 
  LEFT JOIN State sta ON sta.State_ID = cua.State_ID 
WHERE 
  ord.Discount > 0 
ORDER BY 
  Order_date DESC
~~~~

### RQL (Raven Query Language)

~~~~rql
from Orders where Shipping != null and
                  Shipping.Type != “PickUp” and
                  Shipping.Address != null and
                  Shipping.Address.UF in ("“RJ", "MG", “SP", “ES") and
                  Discount > 0.00
order by When desc
select  Number,
        When,
        Items.Count,
        TotalItemsCurrency,
        DiscountPercent,
        TotalCurrency,
        Consumer.Name,
        Shipping.Type,
        Shipping.Status,
        Shipping.Address.UF
~~~~
