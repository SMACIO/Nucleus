using System;
using System.Collections.Generic;

namespace Nucleus.Timing
{
    public interface ITimezoneProvider
    {
        List<NameValue> GetWindowsTimezones();

        List<NameValue> GetIanaTimezones();

        string WindowsToIana(string windowsTimeZoneId);

        string IanaToWindows(string ianaTimeZoneName);

        TimeZoneInfo GetTimeZoneInfo(string windowsOrIanaTimeZoneId);
    }
}

