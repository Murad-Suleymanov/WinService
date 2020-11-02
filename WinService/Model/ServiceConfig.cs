using System;
using System.Xml.Serialization;

namespace WinService.Model
{
    /// <summary>
    /// Configurations for windows service
    /// </summary>
    [XmlRoot("ServiceConfiguration")]
    public class ServiceConfig
    {
        private byte serviceStartHour;

        /// <summary>
        /// Start hour of service
        /// </summary>
        public byte ServiceStartHour
        {
            get => serviceStartHour;
            set
            {
                if (value < 0 || value > 24)
                    throw new ArgumentOutOfRangeException(nameof(value));
                serviceStartHour = value;
            }
        }

        /// <summary>
        /// Timer work interval in minutes
        /// </summary>
        public double WorkInterval { get; set; } = 2;

        /// <summary>
        /// Data fetch interval in minutes
        /// </summary>
        public int FetchInterval { get; set; } = 30;

        /// <summary>
        /// Waiting time after failed connection
        /// </summary>
        public int WaitTime { get; set; } = 10;

        /// <summary>
        /// Number of attempts before throwing exception
        /// </summary>
        public byte AttemptCount { get; set; } = 5;

        /// <summary>
        /// Number of items to be processed at a time
        /// </summary>
        public short CapacityOfPacket { get; set; } = 100;
    }
}
