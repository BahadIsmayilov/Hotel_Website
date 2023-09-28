using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Service_Container.Utulities
{
    public static class Utulitie
    {
        public static bool RemoveImage(string root, string folder, string image)
        {
            string path = Path.Combine(root, "img", folder, image);

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
                return true;
            }
            return false;
        }
    }
}
