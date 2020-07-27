using Covid19.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Covid19.ViewModels
{
    class DirectoryViewModel : ViewModel
    {
        private readonly DirectoryInfo _DirectoryInfo;

        public IEnumerable<DirectoryViewModel> SubDicectories
        {
            get
            {
                try
                {
                    return _DirectoryInfo
                       .EnumerateDirectories()
                       .Select(dir_info => new DirectoryViewModel(dir_info.FullName));
                }
                catch(UnauthorizedAccessException e)
                {
                    Debug.WriteLine(e.ToString());
         
                }
                return Enumerable.Empty<DirectoryViewModel>();
            }

        }
        public IEnumerable<FileViewModel> Files
        {
            get
            {
                try
                {
                    var files = _DirectoryInfo.EnumerateFiles()
                .Select(file => new FileViewModel(file.FullName));
                    return files;
                }
                catch (UnauthorizedAccessException e)
                {
                    Debug.WriteLine(e.ToString());
                    
                }
                return Enumerable.Empty<FileViewModel>();
            }
           
        }


        public IEnumerable<object> DirectoryItems
        {
            get
            {
                try
                {
                    return SubDicectories.Cast<object>().Concat(Files);
                }
                catch (UnauthorizedAccessException e)
                {
                    Debug.WriteLine(e.ToString());
                }
                return Enumerable.Empty<object>();
            }
        }

        public string Name => _DirectoryInfo.Name;

        public string Path => _DirectoryInfo.FullName;

        public DateTime CreationTime => _DirectoryInfo.CreationTime;

        public DirectoryViewModel(string path) => _DirectoryInfo = new DirectoryInfo(path);

    }


    class FileViewModel : ViewModel
    {
        private FileInfo _FileInfo;

        public FileViewModel(string path) => _FileInfo = new FileInfo(path);


        public string Name => _FileInfo.Name;

        public string Path => _FileInfo.FullName;

        public DateTime CreationTime => _FileInfo.CreationTime;
    }
}