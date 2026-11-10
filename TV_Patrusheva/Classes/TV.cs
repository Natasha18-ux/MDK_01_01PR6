using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace TV_Patrusheva.Classes
{
    public class TV
    {
        private int activeChanell;
        private int activeVolume;


        public int ActiveVolume
        {
            get
            {
                return activeVolume;
            }
            set
            {
                if (value > 100) 
                    value = 100;
                if (value < 0) 
                    value = 0;
                activeVolume = value;

            }
        }

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

        // Репазиторий каналов и их передач
        public TV() 
        {
            AllChanells.Add(new Chanell()
            {
                Name = "Музыка года",
                peredacha = new List<string>
                {
                    "G:\\МДК 01.01\\chanel_2_1music.mp4",
                    "G:\\МДК 01.01\\chanel_2_2music.mp4",
                }
            });
            AllChanells.Add(new Chanell()
            {
                Name = "ТВ3",
                peredacha = new List<string>
                {
                    "G:\\МДК 01.01\\chanel_1_1serials.mp4",
                    "G:\\МДК 01.01\\chanel_1_2serials.mp4"
                }
            });
            AllChanells.Add(new Chanell()
            {
                Name = "Пятница",
                peredacha = new List<string>
                {
                    "G:\\МДК 01.01\\chanel_3_1show.mp4",
                    "G:\\МДК 01.01\\chanel_3_2show.mp4",
                    "G:\\МДК 01.01\\chanel_3_3show.mp4"
                }
            });
            
        }


        // метод смены каналов
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
                int videoIndex = AllChanells[ActiveChanell].memoryPeredacha; // берем из списка всех каналов активный и его передачу
                string videoPath = AllChanells[ActiveChanell].peredacha[videoIndex]; // записываем путь из репазитория каналов с указанием индекса передачи 

                mediaElement.Source = new Uri(videoPath);
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


        // поиск канала по номеру
        public void SerchChanel(int chanelNumber, MediaElement mediaElement, Label nameChannel)
        {
            if (chanelNumber <= AllChanells.Count - 1)
            {
                activeChanell = chanelNumber;
                SwapChanell(mediaElement, nameChannel);
            }
            else
                MessageBox.Show("Нет такого канала");
            
        }
        
        // Переключение программ внутри канала
        public void NextProgram(MediaElement mediaElement, Label nameChannel)
        {
            var channel = AllChanells[ActiveChanell]; // Берем активный канал
            channel.memoryPeredacha--;
            if (channel.memoryPeredacha < 0)
                channel.memoryPeredacha = channel.peredacha.Count - 1;
            SwapChanell(mediaElement, nameChannel);
        }

        public void BackProgram(MediaElement mediaElement, Label nameChannel)
        {
            var channel = AllChanells[ActiveChanell]; 
            channel.memoryPeredacha++;
            if (channel.memoryPeredacha >= channel.peredacha.Count)
                channel.memoryPeredacha = 0;
            SwapChanell(mediaElement, nameChannel);
        }


        // переключение самих каналов
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


        // реализация смены звука
        public void UpSound()
        {
            ActiveVolume = ActiveVolume + 5;
        }

        public void DownSound() 
        { 
            ActiveVolume = ActiveVolume - 5; 
        }
    }
}
