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
        public TestAssemblyTests(){

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
        public void UpdateQuality_Less_Than_50_Quality_Increases_By_1(){
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
        public void UpdateQuality_backstagepass_quality_is_zero_when_sellin_is_0()
        {
            var backstage = _app.Items[4];
            backstage.SellIn = 0; 
            Assert.Equal(20, backstage.Quality);
            _app.UpdateQuality(); 
            Assert.Equal(0, backstage.Quality);
        }

        [Fact]
        public void Main_prints_omghai()
        {
            var output = new StringWriter(); 
            System.Console.SetOut(output); 

            Program.Main(new string[]{});
            
            Assert.Equal("OMGHAI!".Trim(), output.GetStringBuilder().ToString().Trim());
            // Tryk "Enter" :-(         
            //kan det undgås??????
            
        }
    }
}