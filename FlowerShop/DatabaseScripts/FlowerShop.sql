USE FlowerShop
GO

--------------------------------------------------
-- Drop tables in order from most dependent to least
DROP TABLE IF EXISTS FlowerProduct;
--------------------------------------------------
GO

--------------------------------------------------
-- Creates table FlowerProduct
CREATE TABLE FlowerProduct (
	Id Int IDENTITY (1,1) NOT NULL,
	Name NVarChar(255) NOT NULL,
	PotSize NVarChar(255) NOT NULL,
	PlantSize NVarChar(255) NOT NULL,
	SalePrice Float NOT NULL,
	PurchasePrice Float NOT NULL,
	Picture VARBINARY(MAX) NOT NULL,
	IsDeleted Bit NOT NULL DEFAULT 0,
	CONSTRAINT PK_FlowerProduct_Id PRIMARY KEY (Id)
)

-- Inserts values into FlowerProduct table
INSERT INTO FlowerProduct (Name, PotSize, PlantSize, SalePrice, PurchasePrice, Picture, IsDeleted)
VALUES
('Cherry Blossom Charm', 'Medium', 'Medium', 189.99, 120.00, 0x, 0),
('Golden Tulips', 'Large', 'Large', 139.50, 90.00, 0x, 0),
('Violet Whispers', 'Small', 'Small', 99.95, 65.00, 0x, 0),
('Crimson Bloom', 'Large', 'Large', 219.00, 140.00, 0x, 0),
('Pastel Petals', 'Medium', 'Medium', 179.45, 110.00, 0x, 0),
('White Orchid Grace', 'Tiny', 'Small', 259.99, 180.00, 0x, 0),
('Garden Glory', 'Large', 'Large', 169.95, 115.00, 0x, 0),
('Pink Peony Bliss', 'Medium', 'Medium', 199.50, 135.00, 0x, 0),
('Sunset Bouquet', 'Tiny', 'Small', 149.95, 90.00, 0x, 0),
('Floral Fantasy', 'Giant', 'Large', 229.00, 150.00, 0x, 0),
('Spring Radiance', 'Medium', 'Medium', 129.95, 85.00, 0x, 0),
('Lavender Lush', 'Small', 'Medium', 114.95, 75.00, 0x, 0),
('Daisy Delight', 'Small', 'Small', 89.99, 60.00, 0x, 0),
('Orchid Majesty', 'Large', 'Large', 274.99, 190.00, 0x, 0),
('Sunburst Blooms', 'Medium', 'Large', 159.95, 100.00, 0x, 0),
('Petal Parade', 'Tiny', 'Small', 74.99, 45.00, 0x, 0),
('Rose Romance', 'Medium', 'Medium', 189.00, 130.00, 0x, 0),
('Peach Perfection', 'Small', 'Small', 104.99, 70.00, 0x, 0),
('Amber Asters', 'Medium', 'Medium', 119.50, 80.00, 0x, 0),
('Heavenly Hydrangea', 'Large', 'Large', 209.95, 145.00, 0x, 0),
('Tulip Treasure', 'Small', 'Small', 94.95, 65.00, 0x, 0),
('Blooming Breeze', 'Medium', 'Medium', 179.95, 120.00, 0x, 0),
('Lilac Love', 'Tiny', 'Small', 124.99, 85.00, 0x, 0),
('Majestic Marigold', 'Large', 'Large', 144.95, 95.00, 0x, 0),
('Daffodil Dream', 'Tiny', 'Small', 84.95, 55.00, 0x, 0),
('Budding Beauty', 'Medium', 'Medium', 199.95, 140.00, 0x, 0),
('Citrus Zest', 'Small', 'Medium', 109.95, 75.00, 0x, 0),
('Mystic Magnolia', 'Large', 'Large', 229.95, 160.00, 0x, 0),
('Petunia Pop', 'Tiny', 'Small', 69.95, 40.00, 0x, 0),
('Glimmer Gardenia', 'Medium', 'Medium', 184.95, 125.00, 0x, 0),
('Twilight Tulip', 'Large', 'Large', 194.95, 135.00, 0x, 0),
('Berry Bloom', 'Small', 'Small', 104.50, 70.00, 0x, 0),
('Orchid Elegance', 'Medium', 'Medium', 239.95, 160.00, 0x, 0),
('Coral Charm', 'Tiny', 'Small', 89.95, 60.00, 0x, 0),
('Sunflower Shine', 'Large', 'Large', 149.99, 100.00, 0x, 0),
('Plum Petals', 'Medium', 'Medium', 119.95, 80.00, 0x, 0),
('Lavish Lilies', 'Large', 'Large', 214.95, 150.00, 0x, 0),
('Peony Perfection', 'Small', 'Small', 99.95, 65.00, 0x, 0),
('Flamingo Fern', 'Tiny', 'Small', 79.95, 50.00, 0x, 0),
('Rose Reflection', 'Medium', 'Medium', 189.95, 130.00, 0x, 0),
('Tropical Treasure', 'Giant', 'Large', 249.95, 170.00, 0x, 0),
('Bluebell Breeze', 'Small', 'Small', 94.99, 65.00, 0x, 0),
('Evergreen Envy', 'Medium', 'Medium', 169.95, 115.00, 0x, 0),
('Golden Garden', 'Large', 'Large', 219.95, 150.00, 0x, 0),
('Poppy Passion', 'Tiny', 'Small', 84.50, 55.00, 0x, 0),
('Mint Marvel', 'Small', 'Small', 109.99, 75.00, 0x, 0),
('Radiant Roses', 'Medium', 'Medium', 204.95, 145.00, 0x, 0),
('Whispering Wisteria', 'Large', 'Large', 239.99, 165.00, 0x, 0),
('Blossom Burst', 'Medium', 'Medium', 179.99, 120.00, 0x, 0),
('Iris Illusion', 'Tiny', 'Small', 94.95, 65.00, 0x, 0);

--------------------------------------------------
