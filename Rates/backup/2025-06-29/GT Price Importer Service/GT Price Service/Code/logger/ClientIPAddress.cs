using System;
using System.Net;
using System.ServiceModel.Channels;

namespace GTPriceImporterService
{
    internal static class ClientIPAddress
    {
        //Not Using
        internal static string GetIP2(System.ServiceModel.OperationContext context)
        {
            try
            {
                System.ServiceModel.Channels.MessageProperties properties = context.IncomingMessageProperties;
                System.ServiceModel.Channels.RemoteEndpointMessageProperty endpoint = properties[System.ServiceModel.Channels.RemoteEndpointMessageProperty.Name] as System.ServiceModel.Channels.RemoteEndpointMessageProperty;

                string address = string.Empty;

                if (properties.Keys.Contains(System.ServiceModel.Channels.HttpRequestMessageProperty.Name))
                {
                    System.ServiceModel.Channels.HttpRequestMessageProperty endpointLoadBalancer = properties[System.ServiceModel.Channels.HttpRequestMessageProperty.Name] as System.ServiceModel.Channels.HttpRequestMessageProperty;
                    if (endpointLoadBalancer != null && endpointLoadBalancer.Headers["X-Forwarded-For"] != null)
                        address = endpointLoadBalancer.Headers["X-Forwarded-For"];
                }

                if (string.IsNullOrEmpty(address))
                {
                    address = endpoint.Address;
                }

                return address;
            }
            catch (Exception)
            {
                return "UNDEFINED IP";
            }
        }

        //OK IPv6 To IPv4
        internal static string GetIP(System.ServiceModel.OperationContext context)
        {
            try
            {
                MessageProperties prop = context.IncomingMessageProperties;
                RemoteEndpointMessageProperty endpoint = prop[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;

                IPAddress address;
                string IPaddress = endpoint.Address;

                if (IPAddress.TryParse(IPaddress, out address))
                {
                    switch (address.AddressFamily)
                    {
                        case System.Net.Sockets.AddressFamily.InterNetwork:
                            return "IPv4 " + IPaddress;
                        case System.Net.Sockets.AddressFamily.InterNetworkV6:
                            //Gets Local IP
                            var ipv4Address = GetIPv4Address(address.ToString());
                            if (string.IsNullOrEmpty(ipv4Address))
                            {
                                return "IPv6 " + IPaddress;
                            }

                            return "IP6 " + IPaddress + " | IP4 " + ipv4Address.ToString();
                        default:
                            return IPaddress;
                    }
                }

                return endpoint.Address;
            }
            catch (Exception)
            {
                return "UNDEFINED IP";
            }
        }

        internal static string GetIPv4Address(string sHostNameOrAddress)
        {
            try
            {
                IPAddress[] aIPHostAddresses = Dns.GetHostAddresses(sHostNameOrAddress);
                foreach (IPAddress ipHost in aIPHostAddresses)
                    if (ipHost.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        return ipHost.ToString();

                foreach (IPAddress ipHost in aIPHostAddresses)
                    if (ipHost.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                    {
                        IPHostEntry ihe = Dns.GetHostEntry(ipHost);
                        foreach (IPAddress ipEntry in ihe.AddressList)
                            if (ipEntry.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                                return ipEntry.ToString();
                    }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex);
            }
            return null;
        }
    }
}