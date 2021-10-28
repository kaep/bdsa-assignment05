using System.Collections.Generic; 

namespace GildedRose.Console
{
    public class Program
    {
        public IList<Item> Items;
        public static void Main(string[] args)
        {
            System.Console.WriteLine("OMGHAI!");
        
            var app = new Program()
            {
                Items = new List<Item>
                                          {
                                                             new Item { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20 },
                                                            new Item { Name = "Aged Brie", SellIn = 2, Quality = 0 },
                                                            new Item { Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7 },
                                                            new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80 },
                                                            new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = -1, Quality = 80 },
                                                            new Item
                                                            {
                                                                Name = "Backstage passes to a TAFKAL80ETC concert",
                                                                SellIn = 15,
                                                                Quality = 20
                                                            },
                                                            new Item
                                                            {
                                                                Name = "Backstage passes to a TAFKAL80ETC concert",
                                                                SellIn = 10,
                                                                Quality = 49
                                                            },
                                                            new Item
                                                            {
                                                                Name = "Backstage passes to a TAFKAL80ETC concert",
                                                                SellIn = 5,
                                                                Quality = 49
                                                            },
				                                            // this conjured item does not work properly yet
				                                            new Item { Name = "Conjured Mana Cake", SellIn = 3, Quality = 6 }
                                          }

            };

            for (var i = 0; i < 31; i++)
            {
                System.Console .WriteLine("-------- day " + i + " --------");
                System.Console.WriteLine("name, sellIn, quality");
                for (var j = 0; j < app.Items.Count; j++)
                {
                    System.Console.WriteLine(app.Items[j].Name + ", " + app.Items[j].SellIn + ", " + app.Items[j].Quality);
                }
                System.Console.WriteLine("");
                app.UpdateQuality();
            }

        }
  


        public void UpdateQuality()
        {
            for (var i = 0; i < Items.Count; i++)
            {
                if (Items[i].Name == "+5 Dexterity Vest" || Items[i].Name == "Elixir of the Mongoose")
                {
                    updateNormalItem(Items[i]);
                }
                if (Items[i].Name == "Backstage passes to a TAFKAL80ETC concert")
                {
                    updateBackstagePass(Items[i]);
                }
                if(Items[i].Name == "Conjured Mana Cake")
                {
                    updatekage(Items[i]);
                }
                if(Items[i].Name == "Aged Brie")
                {
                    updateCheese(Items[i]);
                }
            
                if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
                {
                    Items[i].SellIn = Items[i].SellIn - 1;
                }

            }
        }

        private void updateNormalItem(Item item)
        {
            if (item.Quality > 0)
            {
                item.Quality = item.Quality - 1;
            }
            if (item.Quality > 0 && item.SellIn <= 0)
            {
                item.Quality--;
            }
        }

        private void updateBackstagePass(Item item)
        {
            if (item.Quality < 50)
            {
                item.Quality++;
            }

            if (item.SellIn < 11)
            {
                if (item.Quality < 50)
                {
                    item.Quality = item.Quality + 1;
                }

                if (item.SellIn < 6)
                {
                    if (item.Quality < 50)
                    {
                        item.Quality = item.Quality + 1;
                    }
                }
            }
            if(item.SellIn <= 0)
            {
                item.Quality = 0; 
            }

        }

        private void updateCheese(Item item)
        {
            if (item.Quality < 50)
            {
                if(item.SellIn > 0)
                {
                    item.Quality = item.Quality + 1;
                }
                else
                {
                    item.Quality = item.Quality + 2;
                }
            }
        }

        private void updatekage(Item item)
        {
            if(item.Quality > 1)
            {
                item.Quality -=2; 
            }
            else 
            {
                item.Quality -= item.Quality; 
            }
            if(item.SellIn < 0)
            {
                if(item.Quality > 1)
                {
                    item.Quality -=2;
                }
                else 
                {
                item.Quality -= item.Quality; 
                }
            }
        }

    }

    public class Item
    {
        public string Name { get; set; }

        public int SellIn { get; set; }

        public int Quality { get; set; }
    }

}