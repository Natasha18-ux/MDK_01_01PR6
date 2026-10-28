using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace TV_Patrusheva.Classes
{
    public class TV
    {
        private int activeChanell;
        private int activeVolume;

        public int ActiveChanell
        {
            get { 
                return activeChanell;
            }
            set
            {
                if (activeChanell < AllChanells.Count - 1)
                    activeChanell = value;
                else
                    activeChanell = 0;

                if (activeChanell < 0)
                    activeChanell = AllChanells.Count - 1;
            }
        }

        public List<Chanell> AllChanells = new List<Chanell>();

        public TV()
        {
            AllChanells.Add(new Chanell()
            {
                Name = "Канал1",
                Scr = ""
            });
            AllChanells.Add(new Chanell()
            {
                Name = "Канал2",
                Scr = ""
            });
            AllChanells.Add(new Chanell()
            {
                Name = "Канал3",
                Scr = ""
            });
        }

        public void SwapChanell(MediaElement mediaElement, Label nameChannel)
        {
            DoubleAnimation startAnimation = new DoubleAnimation()
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.6)
            };
            startAnimation.Completed += delegate
            {
                mediaElement.Source = new Uri(AllChanells[ActiveChanell].Scr);
                mediaElement.Play();
                DoubleAnimation endAnimation = new DoubleAnimation()
                {
                    From = 0,
                    To = 1,
                    Duration = TimeSpan.FromSeconds(0.6)
                };
                mediaElement.BeginAnimation(MediaElement.OpacityProperty, endAnimation);
            };

            mediaElement.BeginAnimation(MediaElement.OpacityProperty, startAnimation);
            nameChannel.Content = AllChanells[ActiveChanell].Name;
        }

        public void NextChanell(MediaElement mediaElement, Label nameChannel)
        {
            ActiveChanell++;
            SwapChanell(mediaElement, nameChannel);
        }

        public void BackChanell(MediaElement mediaElement, Label nameChannel)
        {
            ActiveChanell--;
            SwapChanell(mediaElement, nameChannel);
        }

    }
}
