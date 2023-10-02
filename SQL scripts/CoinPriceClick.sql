--количество с группировкой
SELECT "Ip",Count("Ip") FROM "CoinPriceClick"
Group by "Ip"
Order by ("Ip") Desc
