using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Extensions
{
    public static class PolylineDecoderExtension
    {
        public static List<(double Latitude, double Longitude)> Decode(this string encodedPolyline)
        {
            if (string.IsNullOrEmpty(encodedPolyline))
                return default;
            List<(double Latitude, double Longitude)> polylinePoints = new List<(double Latitude, double Longitude)>();

            int index = 0;
            int latitude = 0;
            int longitude = 0;

            while (index < encodedPolyline.Length)
            {
                int shift = 0;
                int result = 0;

                int currentByte;
                do
                {
                    currentByte = encodedPolyline[index++] - 63;
                    result |= (currentByte & 0x1f) << shift;
                    shift += 5;
                } while (currentByte >= 0x20);

                int deltaLatitude = ((result & 1) != 0 ? ~(result >> 1) : (result >> 1));
                latitude += deltaLatitude;

                shift = 0;
                result = 0;

                do
                {
                    currentByte = encodedPolyline[index++] - 63;
                    result |= (currentByte & 0x1f) << shift;
                    shift += 5;
                } while (currentByte >= 0x20);

                int deltaLongitude = ((result & 1) != 0 ? ~(result >> 1) : (result >> 1));
                longitude += deltaLongitude;

                double finalLatitude = latitude * 1e-5;
                double finalLongitude = longitude * 1e-5;

                polylinePoints.Add((finalLatitude, finalLongitude));
            }

            return polylinePoints;
        }
    }
}
