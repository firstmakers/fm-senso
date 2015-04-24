using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HID
{
    public enum  Comando
    {
        Temperatura,
        Luz,
        LuzTemperatura,
        Humedad
    };

    public static class Command {
        public static readonly byte Luz = 0x83;
        public static readonly byte Temperatura = 0x82;
        public static readonly byte LuzTemperatura = 0x81;
        public static readonly byte Humedad = 0x86;

    }
}
