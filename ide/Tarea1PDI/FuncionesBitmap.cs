using System;
using System.Runtime.InteropServices;
using System.IO;
using System.Windows.Forms;

namespace Tarea1PDI
{
    class FuncionesBitmap
    {
        [StructLayout(LayoutKind.Explicit, Size = 14)]
        struct cabeceraBMP
        {
            [FieldOffset(0)]
            public ushort tipoDeArchivo;          
            [FieldOffset(2)]
            public uint tamanio;            
            [FieldOffset(6)]
            public ushort reservado1;    
            [FieldOffset(8)]
            public ushort reservado2;     
            [FieldOffset(10)]
            public uint offset;           
        };

        [StructLayout(LayoutKind.Explicit, Size = 40)]
        public struct infoCabeceraBMP
        {
            [FieldOffset(0)]
            public uint tamanioheader;        
            [FieldOffset(4)]
            public int ancho;    
            [FieldOffset(8)]
            public int alto;      
            [FieldOffset(12)]
            public ushort planos;       
            [FieldOffset(14)]
            public ushort numeroBits;   
            [FieldOffset(16)]
            public uint compresion;   
            [FieldOffset(20)]
            public uint tamanioImagen;    
            [FieldOffset(24)]
            public int pixelPorMetroX;   
            [FieldOffset(28)]
            public int pixelPorMetroY;   
            [FieldOffset(32)]
            public uint numeroDeColores; 
            [FieldOffset(36)]
            public uint coloresImportantes;   
        };

        private cabeceraBMP informacionCabecera;
        private infoCabeceraBMP informacionImagen;
        private byte[] tablaDeColores;
        private byte[] datosDeImagen;
        private string imagePatch;

        private T byteAEstructura<T>(byte[] bytes) where T : struct
        {
            GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            T stuff = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            handle.Free();
            return stuff;
        }

        private byte[] EstructuraAArreglo<T>(T stuff) where T : struct
        {
            int objsize = Marshal.SizeOf(typeof(T));
            byte[] ret = new byte[objsize];
            IntPtr buff = Marshal.AllocHGlobal(objsize);
            Marshal.StructureToPtr(stuff, buff, true);
            Marshal.Copy(buff, ret, 0, objsize);
            Marshal.FreeHGlobal(buff);
            return ret;
        }

        private byte[] GetBytesFileData()
        {
            this.informacionCabecera.tamanio = (uint)14 + (uint)40 + (uint)tablaDeColores.Length + (uint)datosDeImagen.Length;
            this.informacionCabecera.offset = (uint)14 + (uint)40 + (uint)tablaDeColores.Length;
            this.informacionImagen.tamanioheader = 40;
            this.informacionImagen.tamanioImagen = (uint)datosDeImagen.Length;
            this.informacionImagen.numeroDeColores = (uint)(tablaDeColores.Length / 4);
            byte[] informacionCabecera = EstructuraAArreglo<cabeceraBMP>(this.informacionCabecera);
            byte[] informacionImagen = EstructuraAArreglo<infoCabeceraBMP>(this.informacionImagen);
            byte[] outFileData = new byte[informacionCabecera.Length + informacionImagen.Length + tablaDeColores.Length + datosDeImagen.Length];
            Buffer.BlockCopy(informacionCabecera, 0, outFileData, 0, informacionCabecera.Length);
            Buffer.BlockCopy(informacionImagen, 0, outFileData, informacionCabecera.Length, informacionImagen.Length);
            Buffer.BlockCopy(tablaDeColores, 0, outFileData, informacionCabecera.Length + informacionImagen.Length, tablaDeColores.Length);
            Buffer.BlockCopy(datosDeImagen, 0, outFileData, informacionCabecera.Length + informacionImagen.Length + tablaDeColores.Length, datosDeImagen.Length);
            return outFileData;
        }

        private void EscribirPixel(byte[] imagen, int x, int y, uint pixel, int Width = 0)
        {
            int byteToRead, bytesPerRow, bytesUsedPerRow, bytesPaddingPerRow, pixelsPerRow = (Width == 0) ? informacionImagen.ancho : Width;
            switch (informacionImagen.numeroBits)
            {
                case 1:
                    bytesUsedPerRow = (int)Math.Ceiling((double)pixelsPerRow / 8.0);
                    bytesPaddingPerRow = ((bytesUsedPerRow % 4) == 0) ? 0 : 4 - (bytesUsedPerRow % 4);
                    bytesPerRow = bytesUsedPerRow + bytesPaddingPerRow;
                    byteToRead = (y * bytesPerRow) + x / 8;
                    int bitToRead = (7 - (x % 8));
                    imagen[byteToRead] = (byte)(((byte)pixel << bitToRead) | (~(0x1 << bitToRead) & imagen[byteToRead]));
                    break;
                case 4:
                    bytesUsedPerRow = (int)Math.Ceiling((double)pixelsPerRow / 2.0);
                    bytesPaddingPerRow = ((bytesUsedPerRow % 4) == 0) ? 0 : 4 - (bytesUsedPerRow % 4);
                    bytesPerRow = bytesUsedPerRow + bytesPaddingPerRow;
                    byteToRead = (y * bytesPerRow) + x / 2;
                    int bitesToRead = ((x % 2) == 0) ? 4 : 0;
                    imagen[byteToRead] = (byte)(((byte)pixel << bitesToRead) | (~(0xF << bitesToRead) & imagen[byteToRead]));
                    break;
                case 8:
                    bytesUsedPerRow = pixelsPerRow;
                    bytesPaddingPerRow = ((bytesUsedPerRow % 4) == 0) ? 0 : 4 - (bytesUsedPerRow % 4);
                    bytesPerRow = bytesUsedPerRow + bytesPaddingPerRow;
                    byteToRead = (y * bytesPerRow) + x;
                    imagen[byteToRead] = (byte)(0xFF & pixel);
                    break;
                case 16:
                    bytesUsedPerRow = pixelsPerRow * 2;
                    bytesPaddingPerRow = ((bytesUsedPerRow % 4) == 0) ? 0 : 4 - (bytesUsedPerRow % 4);
                    bytesPerRow = bytesUsedPerRow + bytesPaddingPerRow;
                    byteToRead = (y * bytesPerRow) + x * 2;
                    imagen[byteToRead + 1] = (byte)(0xFF & (pixel >> 8));
                    imagen[byteToRead] = (byte)(0xFF & pixel);
                    break;
                default:
                    bytesUsedPerRow = pixelsPerRow * 3;
                    bytesPaddingPerRow = ((bytesUsedPerRow % 4) == 0) ? 0 : 4 - (bytesUsedPerRow % 4);
                    bytesPerRow = bytesUsedPerRow + bytesPaddingPerRow;
                    byteToRead = (y * bytesPerRow) + (x * 3);
                    imagen[byteToRead + 2] = (byte)(0xFF & pixel);
                    imagen[byteToRead + 1] = (byte)(0xFF & (pixel >> 8));
                    imagen[byteToRead] = (byte)(0xFF & (pixel >> 16));
                    break;
            }

        }

        private uint LeerPixel(byte[] imagen, int x, int y)
        {
            int byteToRead, bytesPerRow, bytesUsedPerRow, bytesPaddingPerRow, pixelsPerRow = informacionImagen.ancho;
            switch (informacionImagen.numeroBits)
            {
                case 1:
                    bytesUsedPerRow = (int)Math.Ceiling((double)pixelsPerRow / 8.0);
                    bytesPaddingPerRow = ((bytesUsedPerRow % 4) == 0) ? 0 : 4 - (bytesUsedPerRow % 4);
                    bytesPerRow = bytesUsedPerRow + bytesPaddingPerRow;
                    byteToRead = (y * bytesPerRow) + x / 8;
                    int bitToRead = (7 - (x % 8));
                    return Convert.ToUInt32(0x1 & (imagen[byteToRead] >> bitToRead));
                case 4:
                    bytesUsedPerRow = (int)Math.Ceiling((double)pixelsPerRow / 2.0);
                    bytesPaddingPerRow = ((bytesUsedPerRow % 4) == 0) ? 0 : 4 - (bytesUsedPerRow % 4);
                    bytesPerRow = bytesUsedPerRow + bytesPaddingPerRow;
                    byteToRead = (y * bytesPerRow) + x / 2;
                    int bitesToRead = ((x % 2) == 0) ? 4 : 0;
                    return Convert.ToUInt32(0xF & (imagen[byteToRead] >> bitesToRead));
                case 8:
                    bytesUsedPerRow = pixelsPerRow;
                    bytesPaddingPerRow = ((bytesUsedPerRow % 4) == 0) ? 0 : 4 - (bytesUsedPerRow % 4);
                    bytesPerRow = bytesUsedPerRow + bytesPaddingPerRow;
                    byteToRead = (y * bytesPerRow) + x;
                    return Convert.ToUInt32(0xFF & imagen[byteToRead]);
                case 16:
                    bytesUsedPerRow = pixelsPerRow * 2;
                    bytesPaddingPerRow = ((bytesUsedPerRow % 4) == 0) ? 0 : 4 - (bytesUsedPerRow % 4);
                    bytesPerRow = bytesUsedPerRow + bytesPaddingPerRow;
                    byteToRead = (y * bytesPerRow) + (x * 2);
                    return Convert.ToUInt32((0xFF00 & (imagen[byteToRead] << 8)) | (0x00FF & imagen[byteToRead + 1]));
                default:
                    bytesUsedPerRow = pixelsPerRow * 3;
                    bytesPaddingPerRow = ((bytesUsedPerRow % 4) == 0) ? 0 : 4 - (bytesUsedPerRow % 4);
                    bytesPerRow = bytesUsedPerRow + bytesPaddingPerRow;
                    byteToRead = (y * bytesPerRow) + (x * 3);
                    return Convert.ToUInt32((0xFF0000 & (imagen[byteToRead] << 16)) | (0x00FF00 & (imagen[byteToRead + 1] << 8)) | (0x0000FF & imagen[byteToRead + 2]));
            }
        }

        public void CargarImagen(string fileName)
        {
            try
            {
                FileStream archivoBinario = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                BinaryReader bin = new BinaryReader(archivoBinario);
                    informacionCabecera = byteAEstructura<cabeceraBMP>(bin.ReadBytes(14));
                    informacionImagen = byteAEstructura<infoCabeceraBMP>(bin.ReadBytes(40));

                    if (informacionCabecera.tipoDeArchivo == 0x4D42 && informacionImagen.compresion == 0)
                    {
                        datosDeImagen = new byte[informacionImagen.tamanioImagen];
                        if (informacionImagen.numeroBits != 24)
                            informacionImagen.numeroDeColores = (uint)(0x1 << informacionImagen.numeroBits);
                        bin.BaseStream.Seek(54, SeekOrigin.Begin);
                        tablaDeColores = bin.ReadBytes((int)informacionImagen.numeroDeColores * 4);
                        bin.BaseStream.Seek(informacionCabecera.offset, SeekOrigin.Begin);
                        datosDeImagen = bin.ReadBytes((int)informacionImagen.tamanioImagen);
                        imagePatch = fileName;
                    }
                    bin.Dispose();
                    bin.Close();
            }
            catch (Exception _Exception)
            {
                MessageBox.Show(_Exception.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void NuevaImagen (string file)
        {
            string imagenSalida;
            imagenSalida = "imagensalida.bmp";
            File.Copy(file, imagenSalida, true);

            FileStream archivoBinario = new FileStream(imagenSalida,FileMode.Open, FileAccess.Write);
            BinaryWriter aB = new BinaryWriter(archivoBinario);

            aB.Write(EstructuraAArreglo(informacionCabecera));
            aB.Write(EstructuraAArreglo(informacionImagen));
            aB.Write(tablaDeColores);
            aB.Write(datosDeImagen);

            aB.Dispose();
            aB.Close();
            File.Copy(imagenSalida,file,true);
            File.Delete(imagenSalida);
        }

        public void VoltearVertical()
        {
            for (int y = 0; y < informacionImagen.alto / 2; y++)
            {
                for (int x = 0; x < informacionImagen.ancho; x++)
                {
                    uint pixel_1 = LeerPixel(datosDeImagen, x, y), pixel_2 = LeerPixel(datosDeImagen, x, (informacionImagen.alto - 1) - y);
                    EscribirPixel(datosDeImagen, x, y, pixel_2);
                    EscribirPixel(datosDeImagen, x, (informacionImagen.alto - 1) - y, pixel_1);
                }
            }
        }

        public void VoltearHorizontal()
        {
            for (int y = 0; y < informacionImagen.alto; y++)
            {
                for (int x = 0; x < informacionImagen.ancho / 2; x++)
                {
                    uint pixel_1 = LeerPixel(datosDeImagen, x, y), pixel_2 = LeerPixel(datosDeImagen, (informacionImagen.ancho - 1) - x, y);
                    EscribirPixel(datosDeImagen, x, y, pixel_2);
                    EscribirPixel(datosDeImagen, (informacionImagen.ancho - 1) - x, y, pixel_1);
                }
            }
        }

        public void HacerNegativo()
        {
            if (informacionImagen.numeroBits == 16 || informacionImagen.numeroBits == 8 || informacionImagen.numeroBits == 4 || informacionImagen.numeroBits == 1)
            {
                for (int i = 0; i < tablaDeColores.Length; i += 4)
                {
                    tablaDeColores[i] = Convert.ToByte(255-tablaDeColores[i]);
                    tablaDeColores[i + 1] = Convert.ToByte(255-tablaDeColores[i + 1]);
                    tablaDeColores[i + 2] = Convert.ToByte(255-tablaDeColores[i + 2]);
                }
            }
            else
            {
                for (int i = 0; i < informacionImagen.alto; i++)
                {
                    for (int j = 0; j < informacionImagen.ancho; j++)
                    {
                        EscribirPixel(datosDeImagen, j, i, Convert.ToByte(255-LeerPixel(datosDeImagen, j, i)));
                    }
                }
            }
        }

        public void GirarDerecha()
        {
            int sizeOfNewImage = 0, bytesPerRow, bytesUsedPerRow, bytesPaddingPerRow, pixelsPerRow = informacionImagen.alto;
            switch (informacionImagen.numeroBits)
            {
                case 1:
                    bytesUsedPerRow = (int)Math.Ceiling((double)pixelsPerRow / 8.0);
                    bytesPaddingPerRow = ((bytesUsedPerRow % 4) == 0) ? 0 : 4 - (bytesUsedPerRow % 4);
                    bytesPerRow = bytesUsedPerRow + bytesPaddingPerRow;
                    sizeOfNewImage = bytesPerRow * informacionImagen.ancho;
                    break;
                case 4:
                    bytesUsedPerRow = (int)Math.Ceiling((double)pixelsPerRow / 2.0);
                    bytesPaddingPerRow = ((bytesUsedPerRow % 4) == 0) ? 0 : 4 - (bytesUsedPerRow % 4);
                    bytesPerRow = bytesUsedPerRow + bytesPaddingPerRow;
                    sizeOfNewImage = bytesPerRow * informacionImagen.ancho;
                    break;
                case 8:
                    bytesUsedPerRow = pixelsPerRow;
                    bytesPaddingPerRow = ((bytesUsedPerRow % 4) == 0) ? 0 : 4 - (bytesUsedPerRow % 4);
                    bytesPerRow = bytesUsedPerRow + bytesPaddingPerRow;
                    sizeOfNewImage = bytesPerRow * informacionImagen.ancho;
                    break;
                case 16:
                    bytesUsedPerRow = pixelsPerRow * 2;
                    bytesPaddingPerRow = ((bytesUsedPerRow % 4) == 0) ? 0 : 4 - (bytesUsedPerRow % 4);
                    bytesPerRow = bytesUsedPerRow + bytesPaddingPerRow;
                    sizeOfNewImage = bytesPerRow * informacionImagen.ancho;
                    break;
                case 24:
                    bytesUsedPerRow = pixelsPerRow * 3;
                    bytesPaddingPerRow = ((bytesUsedPerRow % 4) == 0) ? 0 : 4 - (bytesUsedPerRow % 4);
                    bytesPerRow = bytesUsedPerRow + bytesPaddingPerRow;
                    sizeOfNewImage = bytesPerRow * informacionImagen.ancho;
                    break;
            }
            byte[] nuevosdatosDeImagen = new byte[sizeOfNewImage];
            for (int y = 0; y < informacionImagen.alto; ++y)
            {
                for (int x = 0; x < informacionImagen.ancho; ++x)
                {
                    EscribirPixel(nuevosdatosDeImagen, y, (informacionImagen.ancho - 1) - x, LeerPixel(datosDeImagen, x, y), informacionImagen.alto);
                }
            }
            datosDeImagen = nuevosdatosDeImagen;
            int Width = informacionImagen.ancho, Height = informacionImagen.alto;
            informacionImagen.ancho = Height;
            informacionImagen.alto = Width;
        }

        public void Girar180grados()
        {
            for (int y = 0; y < informacionImagen.alto / 2; y++)
            {
                for (int x = 0; x < informacionImagen.ancho; x++)
                {
                    uint pixel_1 = LeerPixel(datosDeImagen, x, y), pixel_2 = LeerPixel(datosDeImagen, (informacionImagen.ancho - 1) - x, (informacionImagen.alto - 1) - y);
                    EscribirPixel(datosDeImagen, x, y, pixel_2);
                    EscribirPixel(datosDeImagen, (informacionImagen.ancho - 1) - x, (informacionImagen.alto - 1) - y, pixel_1);
                }
            }
        }

        public void GirarIzquierda()
        {
            int sizeOfNewImage = 0, bytesPerRow, bytesUsedPerRow, bytesPaddingPerRow, pixelsPerRow = informacionImagen.alto;
            switch (informacionImagen.numeroBits)
            {
                case 1:
                    bytesUsedPerRow = (int)Math.Ceiling((double)pixelsPerRow / 8.0);
                    bytesPaddingPerRow = ((bytesUsedPerRow % 4) == 0) ? 0 : 4 - (bytesUsedPerRow % 4);
                    bytesPerRow = bytesUsedPerRow + bytesPaddingPerRow;
                    sizeOfNewImage = bytesPerRow * informacionImagen.ancho;
                    break;
                case 4:
                    bytesUsedPerRow = (int)Math.Ceiling((double)pixelsPerRow / 2.0);
                    bytesPaddingPerRow = ((bytesUsedPerRow % 4) == 0) ? 0 : 4 - (bytesUsedPerRow % 4);
                    bytesPerRow = bytesUsedPerRow + bytesPaddingPerRow;
                    sizeOfNewImage = bytesPerRow * informacionImagen.ancho;
                    break;
                case 8:
                    bytesUsedPerRow = pixelsPerRow;
                    bytesPaddingPerRow = ((bytesUsedPerRow % 4) == 0) ? 0 : 4 - (bytesUsedPerRow % 4);
                    bytesPerRow = bytesUsedPerRow + bytesPaddingPerRow;
                    sizeOfNewImage = bytesPerRow * informacionImagen.ancho;
                    break;
                case 16:
                    bytesUsedPerRow = pixelsPerRow * 2;
                    bytesPaddingPerRow = ((bytesUsedPerRow % 4) == 0) ? 0 : 4 - (bytesUsedPerRow % 4);
                    bytesPerRow = bytesUsedPerRow + bytesPaddingPerRow;
                    sizeOfNewImage = bytesPerRow * informacionImagen.ancho;
                    break;
                case 24:
                    bytesUsedPerRow = pixelsPerRow * 3;
                    bytesPaddingPerRow = ((bytesUsedPerRow % 4) == 0) ? 0 : 4 - (bytesUsedPerRow % 4);
                    bytesPerRow = bytesUsedPerRow + bytesPaddingPerRow;
                    sizeOfNewImage = bytesPerRow * informacionImagen.ancho;
                    break;
            }
            byte[] nuevosdatosDeImagen = new byte[sizeOfNewImage];
            for (int y = 0; y < informacionImagen.alto; y++)
            {
                for (int x = 0; x < informacionImagen.ancho; x++)
                {
                    EscribirPixel(nuevosdatosDeImagen, (informacionImagen.alto - 1) - y, x, LeerPixel(datosDeImagen, x, y), informacionImagen.alto);
                }
            }
            datosDeImagen = nuevosdatosDeImagen;
            int Width = informacionImagen.ancho, Height = informacionImagen.alto;
            informacionImagen.ancho = Height;
            informacionImagen.alto = Width;
        }
    }
}
