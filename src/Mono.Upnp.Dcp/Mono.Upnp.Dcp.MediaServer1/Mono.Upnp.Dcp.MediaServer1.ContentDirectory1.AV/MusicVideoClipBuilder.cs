// 
// MusicVideoClipBuilder.cs
//  
// Author:
//       Scott Peterson <lunchtimemama@gmail.com>
// 
// Copyright (c) 2009 Scott Peterson
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Mono.Upnp.Dcp.MediaServer1.ContentDirectory1.Av
{
    [ClassName ("musicVideoClip")]
    public class MusicVideoClipBuilder : VideoItemBuilder
    {
        readonly List<PersonWithRole> artists = new List<PersonWithRole> ();
        readonly List<string> albums = new List<string> ();
        readonly List<DateTime> scheduled_start_times = new List<DateTime> ();
        readonly List<DateTime> scheduled_end_times = new List<DateTime> ();
        readonly List<string> contributors = new List<string> ();
        
        [XmlElement ("artist", Namespace = Schemas.UpnpSchema)]
        public ICollection<PersonWithRole> Artists { get { return artists; } }
        
        [XmlElement ("storageMedium", Namespace = Schemas.UpnpSchema)]
        public string StorageMedium { get; set; }
        
        [XmlElement ("album", Namespace = Schemas.UpnpSchema)]
        public ICollection<string> Albums { get { return albums; } }
        
        public ICollection<DateTime> ScheduledStartTimes { get { return scheduled_start_times; } }
        
        public ICollection<DateTime> ScheduledEndTimes { get { return scheduled_end_times; } }
        
        [XmlElement ("contributor", Namespace = Schemas.DublinCoreSchema)]
        public ICollection<string> Contributors { get { return contributors; } }
        
        [XmlElement ("date", Namespace = Schemas.DublinCoreSchema)]
        public string Date { get; set; }
        
        [XmlElement ("scheduledStartTime", Namespace = Schemas.UpnpSchema)]
        IEnumerable<string> StartTimes {
            get { return ToFormattedTimes (scheduled_start_times); }
        }
        
        [XmlElement ("scheduledEndTime", Namespace = Schemas.UpnpSchema)]
        IEnumerable<string> EndTimes {
            get { return ToFormattedTimes (scheduled_end_times); }
        }
    }
}
