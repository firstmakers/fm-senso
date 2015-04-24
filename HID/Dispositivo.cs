using System;
using System.Diagnostics;

namespace HID
{ 
    public class Dispositivo
    {

        Notificacion hid;
        public delegate void DeviceAttachedDelegate();
        public event DeviceAttachedDelegate Conexion;
        public delegate void DeviceDetachedDelegate();
        public delegate void SensorDetachedDelegate();
        public event DeviceDetachedDelegate Desconexion;
        public event SensorDetachedDelegate DesconexionSonda;
        public bool conectado;
 
        public string Id { get; set; }

        public Dispositivo() { 
            hid = new Notificacion();
            Id = "Vid_04d8&Pid_003f";
            hid.DeviceIDToFind = Id;
            hid.DeviceAttached += DispositivoConectado;
            hid.SensorDettached += hid_SensorDettached;
            hid.DeviceDetached += DispositivoDesconectado;
            if (hid.AttachedState && !hid.AttachedButBroken) 
                conectado = true;
                    else
                conectado=false;
            
        }

        private void hid_SensorDettached()
        {
            if (DesconexionSonda != null) DesconexionSonda();
            
        }

        private void DispositivoDesconectado()
        {
            conectado = false;
            if (Desconexion != null) Desconexion();
            
        }
        public void WriteCommand(Comando comando) {
            if (comando == Comando.LuzTemperatura)
                hid.WriteCommand(new byte[] {0x81});
            if (comando == Comando.Temperatura)
                hid.WriteCommand(new byte[] {0x82});
            if (comando == Comando.Luz)
                hid.WriteCommand(new byte[] {0x83});
            if (comando == Comando.Humedad)
                hid.WriteCommand(new byte[] {0x84});
        }

        private void DispositivoConectado()
        {
            conectado = true;
            if (Conexion != null) Conexion();        
        }

        #region Métodos Públicos
        /// <summary>
        /// Inicia la medición de datos mediante un parámetro
        /// </summary>
        /// <param name="command">Comando de medición</param>
        public void ComenzarMedicion(byte command) {
            hid.RunWorker(command);
        }

        public bool isRunning() {
           return hid.isBussy();
        }
        /// <summary>
        /// detiene la medicion de datos
        /// </summary>
        public void DetenerMedicion() {
            hid.StopWorker();
        }

        public double getTemp()
        {
            uint t = hid.Temperature;
            Debug.WriteLine("Decimal sin signo : " + t);
            //Levanta evento de desconexión de la sonda de temperatura

            if (hid.LastTemperature == 0 || hid.Temperature == 1360 || hid.Temperature == 32767)
                return 400;         
            if (t != 0)
            {
                if (isNegative(t))
                {
                    string  x = Convert.ToString(~Convert.ToInt32(t) ^ 1, 2).Remove(0, 16);
                    double negative = - Convert.ToInt32(x,2)/16;
                    return Math.Round(negative, 1);
                    
                }
                return Math.Round((t * 0.0625), 1);
            }
            return 0;
        }
        public double getHum()
        {
            return hid.Humidity;         
        }
        public double getLight(){ 
            uint l = hid.Light;
            if(l!=0)
                return Math.Round((l / 1.2), 1);
            return 0;
        }
        #endregion
        private bool isNegative(uint entero)
        {
            string binario = Convert.ToString(entero, 2);
            Debug.WriteLine("TEMP BINARIO:" + binario + " longitud: "+ binario.Length);
            if (binario.Length > 15)
                return true;
            return false;
        }

        private double approximateTemp(double t) {
            double trunk = Math.Truncate(t);
            double d = t - trunk;
            double aux = 0;
            if (d > 0.8)
                aux = 1;
            else if (d <= 0.8 && d >= 0.3)
                aux = 0.5;
            if (trunk < 0)
                return trunk - aux;
            return trunk + aux;
        }



    }
}
