using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace FileSearchApp.lib {
    public class FileSearchException:Exception {
        public FileSearchException() { }
        public FileSearchException(string message) : base(message) { }
        public FileSearchException(string message, Exception inner) : base(message, inner) { }
    }
}
