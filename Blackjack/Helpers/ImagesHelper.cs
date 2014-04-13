using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Imaging;

namespace Blackjack.Helpers
{
    public class ImagesHelper
    {
        public static BitmapImage RandomColorCard(BitmapImage[] images)
        {
            Random ran = new Random();

            return images[ran.Next(0, images.Count())];
        } 

        public static BitmapImage CreateImage(string imageName)
        {
            return new BitmapImage(new Uri("/Images/" + imageName + ".png", UriKind.Relative));
        }

        public static Dictionary<int, BitmapImage[]>  GetBlackJackCards()
        {
            Dictionary<int, BitmapImage[]> images = new Dictionary<int,BitmapImage[]>();

            images.Add(2, CreateImagesArray(CreateImage("h2"), CreateImage("c2"), CreateImage("d2"), CreateImage("s2")));
            images.Add(3, CreateImagesArray(CreateImage("h3"), CreateImage("c3"), CreateImage("d3"), CreateImage("s3")));
            images.Add(4, CreateImagesArray(CreateImage("h4"), CreateImage("c4"), CreateImage("d4"), CreateImage("s4")));
            images.Add(5, CreateImagesArray(CreateImage("h5"), CreateImage("c5"), CreateImage("d5"), CreateImage("s5")));
            images.Add(6, CreateImagesArray(CreateImage("h6"), CreateImage("c6"), CreateImage("d6"), CreateImage("s6")));
            images.Add(7, CreateImagesArray(CreateImage("h7"), CreateImage("c7"), CreateImage("d7"), CreateImage("s7")));
            images.Add(8, CreateImagesArray(CreateImage("h8"), CreateImage("c8"), CreateImage("d8"), CreateImage("s8")));
            images.Add(9, CreateImagesArray(CreateImage("h9"), CreateImage("c9"), CreateImage("d9"), CreateImage("s9")));
            images.Add(11, CreateImagesArray(CreateImage("h1"), CreateImage("c1"), CreateImage("d1"), CreateImage("s1")));

            images.Add(10, new BitmapImage[16] { 
                CreateImage("h10"), CreateImage("c10"), CreateImage("d10"), CreateImage("s10"), CreateImage("hj"), CreateImage("cj"),
                CreateImage("dj"), CreateImage("sj"), CreateImage("hq"), CreateImage("cq"), CreateImage("dq"), CreateImage("sq"),
                CreateImage("hk"), CreateImage("ck"), CreateImage("dk"), CreateImage("sk") });

            return images;
        }

        private static BitmapImage[] CreateImagesArray(BitmapImage one, BitmapImage two, BitmapImage three, BitmapImage four)
        {
            return new BitmapImage[4] { one, two, three, four };
        }
    }
}
