using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMiddleWareTrial.App_Code
{

    public class MusicData
    {
        public string Name { get; set; }
        public string Genre { get; set; }
        public string Composer { get; set; }

        public MusicData(string name, string genre, string composer)
        {
            this.Name = name;
            this.Genre = genre;
            this.Composer = composer;
        }

        public override string ToString()
        {
            return string.Format("Name:{0}, Genre:{1}, Composer:{2}",
                this.Name, this.Genre, this.Composer);
        }

    }

}
