﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace It.Uniba.Di.Cdg.SocialTfs.ServiceLibrary
{
    internal class OAuth2Service
    {
        #region Attributes

        protected string _host;
        protected string _consumerKey;
        protected string _consumerSecret;
        protected string _accessToken;
        protected string _accessSecret;

        #endregion

        /// <summary>
        /// Make a web request with method GET and returns the response as a string.
        /// </summary>
        /// <param name="url">URL for the request.</param>
        /// <returns>Response</returns>
        /// 


        private String WebRequest2(string url)
        {
            url += (String.IsNullOrEmpty(new Uri(url).Query) ? "?" : "&") + "access_token=" + _accessToken;
            HttpWebRequest webRequest = System.Net.WebRequest.Create(url) as HttpWebRequest;
            webRequest.Method = "GET";
            webRequest.ServicePoint.Expect100Continue = false;
            var stream = webRequest.GetResponse().GetResponseStream();
            MemoryStream m = new MemoryStream();
            new System.IO.Compression.GZipStream(stream, System.IO.Compression.CompressionMode.Decompress).CopyTo(m);
            try
            {
                string str = Encoding.UTF8.GetString(m.ToArray());
                return str;
            }
            finally
            {
                if (stream != null)
                    ((IDisposable)stream).Dispose();
            }
        }

        protected String WebRequest(string url)
        {
            url += (String.IsNullOrEmpty(new Uri(url).Query) ? "?" : "&") + "access_token=" + _accessToken;
            HttpWebRequest webRequest = System.Net.WebRequest.Create(url) as HttpWebRequest;
            webRequest.Method = "GET";
            webRequest.ServicePoint.Expect100Continue = false;
            try
            {
                using (StreamReader responseReader = new StreamReader(webRequest.GetResponse().GetResponseStream()))
                    return responseReader.ReadToEnd();
            }
            catch
            {
                return String.Empty;
            }
        }

        protected String WebRequestPost(String url, Dictionary<String, String> bodyParam)
        {
            string data = "{";
            int counter = 0;
            foreach (KeyValuePair<string, string> pair in bodyParam)
            {
                data += "\"" + pair.Key + "\" : \"" + pair.Value + "\"";

                if ((bodyParam.Keys.Count - 1) != counter)
                {
                    data += " , ";
                }

                counter += 1;
            }
            data += "}";

            

            
            byte[] dataStream = Encoding.UTF8.GetBytes(data);

            
            HttpWebRequest webRequest = (HttpWebRequest)System.Net.WebRequest.Create(url);
            webRequest.Method = "POST";
            webRequest.ContentType = "application/json";
            webRequest.ContentLength = dataStream.Length;
            webRequest.Accept = "application/json";
            Stream newStream = webRequest.GetRequestStream();
            newStream.Write(dataStream, 0, dataStream.Length);
            newStream.Close();
            try
            {
                using (StreamReader responseReader = new StreamReader(webRequest.GetResponse().GetResponseStream()))
                    return responseReader.ReadToEnd();
            }
            catch
            {
                 return String.Empty;
            }
        }
    }
}
