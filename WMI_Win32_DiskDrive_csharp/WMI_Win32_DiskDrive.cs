using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace WMI_Win32_DiskDrive_csharp
{
    /// <summary>
    /// Win32_DiskDrive 정보를 담는 Singleton 클래스
    /// </summary>
    /// 
    /// <example>
    ///     // Case #1 초기화
    ///     var win32diskDrive = WMI_Win32_DiskDrive.Instance;
    ///     
    ///     // Case #2 필요한 경우 정보 갱신하기
    ///     win32diskDrive.Refresh();
    ///     
    ///     // Case #3 모든 디스크의 모든 정보 출력
    ///     for (int i = 0; i < win32diskDrive.Count; i++)
    ///     {
    ///       Console.WriteLine(win32diskDrive[i].ToString());
    ///     }
    ///     
    ///     // Case #4 Index 속성이 0인 디스크의 정보 가져오기
    ///     var info = win32diskDrive.GetByDiskIndex(0);
    ///     
    ///     // Case #5 DeviceID 속성이 "\\.\PHYSICALDRIVE2"인 디스크의 정보 가져오기
    ///     var info = diskDrive.GetByDeviceID("\\.\PHYSICALDRIVE2");
    ///     
    ///     // Case #6 Name 속성이 "\\.\PHYSICALDRIVE2"인 디스크의 정보 가져오기
    ///     var info = diskDrive.GetByName("\\.\PHYSICALDRIVE2");
    ///     
    ///     // Case #7 Generic List의 Find() 메서드를 직접 활용
    ///     var info = diskDrive.ToList().Find(o => o.DeviceID == "\\.\PHYSICALDRIVE2");
    ///
    /// </example>
    /// 
    /// <remarks>
    /// Win32_DiskDrive 클래스의 일부 속성은 제외되었다.
    /// </remarks>
    public class WMI_Win32_DiskDrive
    {
        ///
        /// private 멤버변수
        ///
        private List<Win32_DiskDrive_Info> _win32_diskdrives;
        public int Count { get { return _win32_diskdrives.Count; } }
        public Win32_DiskDrive_Info GetByDiskIndex(int index)
        {
            return _win32_diskdrives.Find(x => x.Index == index);
        }
        public Win32_DiskDrive_Info GetByDeviceID(string deviceID)
        {
            return _win32_diskdrives.Find(x => x.DeviceID == deviceID);
        }
        public Win32_DiskDrive_Info GetByName(string name)
        {
            return _win32_diskdrives.Find(x => x.Name == name);
        }

        // singletone pattern
        private static WMI_Win32_DiskDrive _instance;
        public static WMI_Win32_DiskDrive Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new WMI_Win32_DiskDrive();
                }
                return _instance;
            }
        }

        /// <summary>
        /// 생성자
        /// </summary>
        private WMI_Win32_DiskDrive()
        {
            // 멤버변수 초기화
            _win32_diskdrives = new List<Win32_DiskDrive_Info>();

            // WMI를 이용하여 Win32_DiskDrive 정보를 가져온다.
            initialize();
        }

        /// 
        /// public 메서드
        /// 
        public void Refresh()
        {
            initialize();
        }

        public List<Win32_DiskDrive_Info> ToList()
        {
            return new List<Win32_DiskDrive_Info>(_win32_diskdrives);
        }

        /// 
        /// private 메서드
        /// 
        private void initialize()
        {
            _win32_diskdrives.Clear();

            var query = new SelectQuery("Win32_DiskDrive");
            var searcher = new ManagementObjectSearcher(query);
            var physicalDriveList = searcher.Get().Cast<ManagementObject>().ToList();

            foreach (var item in physicalDriveList)
            {
                Win32_DiskDrive_Info newItem = new Win32_DiskDrive_Info();

                newItem.BytesPerSector = (UInt32)(item["BytesPerSector"] ?? (UInt32)0);
                newItem.Caption = ((string)item["Caption"] ?? "").Trim();
                newItem.CompressionMethod = ((string)item["CompressionMethod"] ?? "").Trim();
                newItem.ConfigManagerErrorCode = (UInt32)(item["ConfigManagerErrorCode"] ?? (UInt32)0);
                newItem.ConfigManagerUserConfig = (bool)(item["ConfigManagerUserConfig"] ?? false);
                newItem.CreationClassName = ((string)item["CreationClassName"] ?? "").Trim();
                newItem.DefaultBlockSize = (UInt64)(item["DefaultBlockSize"] ?? (UInt64)0);
                newItem.Description = ((string)item["Description"] ?? "").Trim();
                newItem.DeviceID = ((string)item["DeviceID"] ?? "").Trim();
                newItem.ErrorCleared = (bool)(item["ErrorCleared"] ?? false);
                newItem.ErrorDescription = ((string)item["ErrorDescription"] ?? "").Trim();
                newItem.ErrorMethodology = ((string)item["ErrorMethodology"] ?? "").Trim();
                newItem.FirmwareRevision = ((string)item["FirmwareRevision"] ?? "").Trim();
                newItem.Index = (UInt32)(item["Index"] ?? (UInt32)0);
                newItem.InterfaceType = ((string)item["InterfaceType"] ?? "").Trim();
                newItem.LastErrorCode = (UInt32)(item["LastErrorCode"] ?? (UInt32)0);
                newItem.Manufacturer = ((string)item["Manufacturer"] ?? "").Trim();
                newItem.MaxBlockSize = (UInt64)(item["MaxBlockSize"] ?? (UInt64)0);
                newItem.MaxMediaSize = (UInt64)(item["MaxMediaSize"] ?? (UInt64)0);
                newItem.MediaLoaded = (bool)(item["MediaLoaded"] ?? false);
                newItem.MediaType = ((string)item["MediaType"] ?? "").Trim();
                newItem.MinBlockSize = (UInt64)(item["MinBlockSize"] ?? (UInt64)0);
                newItem.Model = ((string)item["Model"] ?? "").Trim();
                newItem.Name = ((string)item["Name"] ?? "").Trim();
                newItem.NeedsCleaning = (bool)(item["NeedsCleaning"] ?? false);
                newItem.NumberOfMediaSupported = (UInt32)(item["NumberOfMediaSupported"] ?? (UInt32)0);
                newItem.Partitions = (UInt32)(item["Partitions"] ?? (UInt32)0);
                newItem.PNPDeviceID = ((string)item["PNPDeviceID"] ?? "").Trim();
                newItem.PowerManagementSupported = (bool)(item["PowerManagementSupported"] ?? false);
                newItem.SCSIBus = (UInt32)(item["SCSIBus"] ?? (UInt32)0);
                newItem.SCSILogicalUnit = (UInt16)(item["SCSILogicalUnit"] ?? (UInt16)0);
                newItem.SCSIPort = (UInt16)(item["SCSIPort"] ?? (UInt16)0);
                newItem.SCSITargetId = (UInt16)(item["SCSITargetId"] ?? (UInt16)0);
                newItem.SectorsPerTrack = (UInt32)(item["SectorsPerTrack"] ?? (UInt32)0);
                newItem.SerialNumber = ((string)item["SerialNumber"] ?? "").Trim();
                newItem.Signature = (UInt32)(item["Signature"] ?? (UInt32)0);
                newItem.Size = (UInt64)(item["Size"] ?? (UInt64)0);
                newItem.Status = ((string)item["Status"] ?? "").Trim();
                newItem.StatusInfo = (UInt16)(item["StatusInfo"] ?? (UInt16)0);
                newItem.SystemCreationClassName = ((string)item["SystemCreationClassName"] ?? "").Trim();
                newItem.SystemName = ((string)item["SystemName"] ?? "").Trim();
                newItem.TotalCylinders = (UInt64)(item["TotalCylinders"] ?? (UInt64)0);
                newItem.TotalHeads = (UInt32)(item["TotalHeads"] ?? (UInt32)0);
                newItem.TotalSectors = (UInt64)(item["TotalSectors"] ?? (UInt64)0);
                newItem.TotalTracks = (UInt64)(item["TotalTracks"] ?? (UInt64)0);
                newItem.TracksPerCylinder = (UInt32)(item["TracksPerCylinder"] ?? (UInt32)0);
                //win32_DiskDrive.Availability = (ushort)physicalDrive["Availability"];
                //newItem.InstallDate = (DateTime)item["InstallDate"] ?? DateTime.MinValue.ToLocalTime();
                //newItem.InstallDate = (string)item["InstallDate"] ?? "";
                //newItem.PowerManagementCapabilities = (UInt16)(item["PowerManagementCapabilities[]"] ?? 0);

                _win32_diskdrives.Add(newItem);
            }

            _win32_diskdrives = _win32_diskdrives.OrderBy(o => o.Name).ToList();
        }

        ///
        /// Operator 재정의
        ///
        public Win32_DiskDrive_Info this[int index]
        {
            get { return _win32_diskdrives[index]; }
        }

        ///
        /// Override
        ///
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in _win32_diskdrives)
            {
                sb.AppendLine(item.ToString());
            }
            return sb.ToString();
        }

    }

    /// 
    /// Inner class
    /// 
    public class Win32_DiskDrive_Info
    {
        public UInt16 Availability;

        public UInt32 BytesPerSector;

        public string Caption;
        public string CompressionMethod;
        public UInt32 ConfigManagerErrorCode;
        public bool ConfigManagerUserConfig;
        public string CreationClassName;

        public UInt64 DefaultBlockSize;
        public string Description;
        public string DeviceID;

        public bool ErrorCleared;
        public string ErrorDescription;
        public string ErrorMethodology;

        public string FirmwareRevision;

        public UInt32 Index;
        public string InterfaceType;

        public UInt32 LastErrorCode;

        public string Manufacturer;
        public UInt64 MaxBlockSize;
        public UInt64 MaxMediaSize;
        public bool MediaLoaded;
        public string MediaType;
        public UInt64 MinBlockSize;
        public string Model;

        public string Name;
        public bool NeedsCleaning;
        public UInt32 NumberOfMediaSupported;

        public UInt32 Partitions;
        public string PNPDeviceID;
        public bool PowerManagementSupported;

        public UInt32 SCSIBus;
        public UInt16 SCSILogicalUnit;
        public UInt16 SCSIPort;
        public UInt16 SCSITargetId;
        public UInt32 SectorsPerTrack;
        public string SerialNumber;
        public UInt32 Signature;
        public UInt64 Size;
        public string Status;
        public UInt16 StatusInfo;
        public string SystemCreationClassName;
        public string SystemName;

        public UInt64 TotalCylinders;
        public UInt32 TotalHeads;
        public UInt64 TotalSectors;
        public UInt64 TotalTracks;
        public UInt32 TracksPerCylinder;
        //public UInt16 Capabilities[];
        //public string CapabilityDescriptions[];
        //public string InstallDate;
        //public UInt16 PowerManagementCapabilities[];

        public string ToShortString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{Description}{Index}, {Caption}, {SerialNumber}, ({GetHumanReadableDiskSize(Size)}, {InterfaceType})");
            return sb.ToString();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Win32_DiskDrive {");

            sb.AppendLine($"Name={Name}");

            sb.AppendLine($"BytesPerSector={BytesPerSector.ToString()}");

            sb.AppendLine($"Caption={Caption}");
            sb.AppendLine($"CompressionMethod={CompressionMethod}");
            sb.AppendLine($"ConfigManagerErrorCode={ConfigManagerErrorCode.ToString()}");
            sb.AppendLine($"ConfigManagerUserConfig={ConfigManagerUserConfig.ToString()}");
            sb.AppendLine($"CreationClassName={CreationClassName}");

            sb.AppendLine($"DefaultBlockSize={DefaultBlockSize.ToString()}");
            sb.AppendLine($"Description={Description}");
            sb.AppendLine($"DeviceID={DeviceID}");

            sb.AppendLine($"ErrorCleared={ErrorCleared.ToString()}");
            sb.AppendLine($"ErrorDescription={ErrorDescription}");
            sb.AppendLine($"ErrorMethodology={ErrorMethodology}");

            sb.AppendLine($"FirmwareRevision={FirmwareRevision}");

            sb.AppendLine($"Index={Index.ToString()}");
            sb.AppendLine($"InterfaceType={InterfaceType}");

            sb.AppendLine($"LastErrorCode={LastErrorCode.ToString()}");

            sb.AppendLine($"Manufacturer={Manufacturer}");
            sb.AppendLine($"MaxBlockSize={MaxBlockSize.ToString()}");
            sb.AppendLine($"MaxMediaSize={MaxMediaSize.ToString()}");
            sb.AppendLine($"MediaLoaded={MediaLoaded.ToString()}");
            sb.AppendLine($"MediaType={MediaType}");
            sb.AppendLine($"MinBlockSize={MinBlockSize.ToString()}");
            sb.AppendLine($"Model={Model}");

            sb.AppendLine($"NeedsCleaning={NeedsCleaning.ToString()}");
            sb.AppendLine($"NumberOfMediaSupported={NumberOfMediaSupported.ToString()}");

            sb.AppendLine($"Partitions={Partitions.ToString()}");
            sb.AppendLine($"PNPDeviceID={PNPDeviceID}");
            sb.AppendLine($"PowerManagementSupported={PowerManagementSupported.ToString()}");

            sb.AppendLine($"SCSIBus={SCSIBus.ToString()}");
            sb.AppendLine($"SCSILogicalUnit={SCSILogicalUnit.ToString()}");
            sb.AppendLine($"SCSIPort={SCSIPort.ToString()}");
            sb.AppendLine($"SCSITargetId={SCSITargetId.ToString()}");
            sb.AppendLine($"SectorsPerTrack={SectorsPerTrack.ToString()}");
            sb.AppendLine($"SerialNumber={SerialNumber}");
            sb.AppendLine($"Signature={Signature.ToString()}");
            sb.AppendLine($"Size={Size.ToString()}");
            sb.AppendLine($"Status={Status}");
            sb.AppendLine($"StatusInfo={StatusInfo.ToString()}");
            sb.AppendLine($"SystemCreationClassName={SystemCreationClassName}");
            sb.AppendLine($"SystemName={SystemName}");

            sb.AppendLine($"TotalCylinders={TotalCylinders.ToString()}");
            sb.AppendLine($"TotalHeads={TotalHeads.ToString()}");
            sb.AppendLine($"TotalSectors={TotalSectors.ToString()}");
            sb.AppendLine($"TotalTracks={TotalTracks.ToString()}");
            sb.AppendLine($"TracksPerCylinder={TracksPerCylinder.ToString()}");
            sb.AppendLine("}");
            //sb.AppendLine($"InstallDate={InstallDate.ToString()}");
            //sb.AppendLine({PowerManagementCapabilities[]}");
            return sb.ToString();
        }

        static string GetHumanReadableDiskSize(ulong byteSize)
        {
            string[] units = new string[] { "B", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
            int idx = 0;
            double dHumanReadableSize = byteSize;

            while (dHumanReadableSize >= 1024 && idx < units.Length - 1)
            {
                dHumanReadableSize /= 1024;
                idx++;
            }

            string sizeStr = dHumanReadableSize.ToString(idx > 0 ? "#.##" : "#");
            return string.Format("{0}{1}", sizeStr, units[idx]);
        }
    }
}
