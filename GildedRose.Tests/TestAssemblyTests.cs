using System.Runtime.CompilerServices;
using Xunit;
using GildedRose.Console;
using System.Collections.Generic;
using System;
using System.IO;

namespace GildedRose.Tests
{
    public class TestAssemblyTests
    {

        private readonly Program _app;
        public TestAssemblyTests()
        {
            var app = new Program()
            {
                Items = new List<Item>

                                          {
                                              new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
                                              new Item {Name = "Aged Brie", SellIn = 2, Quality = 0},
                                              new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
                                              new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
                                              new Item
                                                  {
                                                      Name = "Backstage passes to a TAFKAL80ETC concert",
                                                      SellIn = 15,
                                                      Quality = 20
                                                  },
                                              new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
                                          }


            };
            _app = app;

        }

        [Fact]
        public void UpdateQuality_normal_item()
        {
            var vest = _app.Items[0];
            _app.UpdateQuality();
            Assert.Equal(19, vest.Quality);
        }

        [Fact]
        public void UpdateQuality_normal_item_quality_zero()
        {
            var vest = _app.Items[0];
            vest.Quality = 0;
            _app.UpdateQuality();
            Assert.Equal(0, vest.Quality);
        }

        [Fact]
        public void UpdateQuality_normal_item_sellin_negative()
        {
            var vest = _app.Items[0];
            vest.SellIn = -1;
            _app.UpdateQuality();
            Assert.Equal(18, vest.Quality);
        }

        [Fact]
        public void UpdateQuality_given_6_updates_two_before_sellin_4_after_aged_brie_quality_increases_by_10()
        {
            var brie = _app.Items[1];
            Assert.Equal(0, brie.Quality);
            Assert.Equal(2, brie.SellIn);
            _app.UpdateQuality();
            _app.UpdateQuality();
            _app.UpdateQuality();
            _app.UpdateQuality();
            _app.UpdateQuality();
            _app.UpdateQuality();
            Assert.Equal(10, brie.Quality);
            Assert.Equal(-4, brie.SellIn);
        }

        [Fact]
        public void UpdateQuality_sulfaras_doesnt_degrade()
        {
            var sulfaras = _app.Items[3];
            Assert.Equal(80, sulfaras.Quality);

            Assert.Equal(0, sulfaras.SellIn);
            _app.UpdateQuality();
            Assert.Equal(80, sulfaras.Quality);
            Assert.Equal(0, sulfaras.SellIn);
        }

        [Fact]
        public void UpdateQuality_non_sulfaras_item_sellin_decrements()
        {
            var vest = _app.Items[0];
            _app.UpdateQuality();
            Assert.Equal(9, vest.SellIn);
        }

        [Fact]
        public void UpdateQuality_Less_Than_50_Quality_Increases_By_1()
        {
            var backstage = _app.Items[4];
            Assert.Equal(20, backstage.Quality);
            _app.UpdateQuality();
            Assert.Equal(21, backstage.Quality);

        }

        [Fact]
        public void UpdateQuality_backstagepass_quality_increases_by_2_when_between_10_and_5_days()
        {
            var backstage = _app.Items[4];

            backstage.SellIn = 10;
            Assert.Equal(20, backstage.Quality);
            _app.UpdateQuality();
            Assert.Equal(22, backstage.Quality);
        }

        [Fact]
        public void UpdateQuality_backstagepass_quality_increases_by_3_when_between_6_and_0_days()
        {
            var backstage = _app.Items[4];
            backstage.SellIn = 5;
            Assert.Equal(20, backstage.Quality);
            _app.UpdateQuality();
            Assert.Equal(23, backstage.Quality);
        }

        [Fact]
        public void UpdateQuality_backstagepass_quality_is_zero_when_sellin_is_negative()
        {
            var backstage = _app.Items[4];
            backstage.SellIn = -1;
            Assert.Equal(20, backstage.Quality);
            _app.UpdateQuality();
            Assert.Equal(0, backstage.Quality);
        }

        [Fact]
        public void UpdateQuality_mana_cakes_degrades_at_double_rate()
        {
            var cake = _app.Items[5];
            _app.UpdateQuality();
            Assert.Equal(4, cake.Quality);
        }

        [Fact]
        public void UpdateQuality_overdue_mana_cakes_degrade_4_points()
        {
            var cake = _app.Items[5];
            cake.SellIn = -1; 
            _app.UpdateQuality();
            Assert.Equal(2, cake.Quality);
        }

        [Fact]
        public void Main_print_of_items_identical_to_print_from_before_refactor()
        {
            var output = new StringWriter();
            System.Console.SetOut(output); 
            
            Program.Main(new string[0]); 

            var directory = Directory.GetCurrentDirectory();
            var expected = System.IO.File.ReadAllText(@"..\..\..\main_text.txt");

            Assert.Equal(expected, output.GetStringBuilder().ToString()); 
        }
    }
}