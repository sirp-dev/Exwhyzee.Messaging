﻿using Newtonsoft.Json;
using Exwhyzee.Messaging.Web.PayStack.Models.Transfers.Initiation;
using Exwhyzee.Messaging.Web.PayStack.Models.Transfers.Recipient;
using Exwhyzee.Messaging.Web.PayStack.Models.Transfers.TransferDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Exwhyzee.Messaging.Web.PayStack.Transfers
{
    public class PaystackTransfers : ITransfers
    {
        string _secretKey;
        public PaystackTransfers(string secretKey)
        {
            this._secretKey = secretKey;
        }

        public async Task<TransferRecipientModel> CreateTransferRecipient(string type, string name, string account_number, 
            string bank_code, string currency = "NGN", string description = null)
        {
            var client = HttpConnection.CreateClient(this._secretKey);

            var bodyKeyValues = new List<KeyValuePair<string, string>>();

            bodyKeyValues.Add(new KeyValuePair<string, string>("type", type));
            bodyKeyValues.Add(new KeyValuePair<string, string>("name", name));
            bodyKeyValues.Add(new KeyValuePair<string, string>("account_number", account_number));
            bodyKeyValues.Add(new KeyValuePair<string, string>("bank_code", bank_code));
            bodyKeyValues.Add(new KeyValuePair<string, string>("currency", currency));

            if (!string.IsNullOrWhiteSpace(description))
            {
                bodyKeyValues.Add(new KeyValuePair<string, string>("description", description));
            }
                      

            var formContent = new FormUrlEncodedContent(bodyKeyValues);

            var response = await client.PostAsync("transferrecipient", formContent);

            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TransferRecipientModel>(json);
        }

      

        public async Task<TransferRecipientListModel> ListTransferRecipients()
        {
            var client = HttpConnection.CreateClient(this._secretKey);
            var response = await client.GetAsync("transferrecipient");

            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TransferRecipientListModel>(json);
        }


        public async Task<TransferInitiationModel> InitiateTransfer(int amount, string recipient_code, string source = "balance", string currency = "NGN", string reason = null)
        {
            var client = HttpConnection.CreateClient(this._secretKey);

            var bodyKeyValues = new List<KeyValuePair<string, string>>();

            bodyKeyValues.Add(new KeyValuePair<string, string>("source", source));
            bodyKeyValues.Add(new KeyValuePair<string, string>("amount", amount.ToString()));
            bodyKeyValues.Add(new KeyValuePair<string, string>("recipient_code", recipient_code));
            bodyKeyValues.Add(new KeyValuePair<string, string>("currency", currency));
            
            if (!string.IsNullOrWhiteSpace(reason))
            {
                bodyKeyValues.Add(new KeyValuePair<string, string>("reason", reason));
            }


            var formContent = new FormUrlEncodedContent(bodyKeyValues);

            var response = await client.PostAsync("transfer", formContent);

            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TransferInitiationModel>(json);
        }

    

        public async Task<TransferDetailsModel> FetchTransfer(string transfer_code)
        {
            var client = HttpConnection.CreateClient(this._secretKey);
            var response = await client.GetAsync($"transfer/{transfer_code}");

            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TransferDetailsModel>(json);
        }

        public async Task<TransferDetailsListModel> ListTransfers()
        {
            var client = HttpConnection.CreateClient(this._secretKey);
            var response = await client.GetAsync("transfer");

            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TransferDetailsListModel>(json);
        }

        public async Task<string> FinalizeTransfer(string transfer_code, string otp)
        {
            var client = HttpConnection.CreateClient(this._secretKey);

            var bodyKeyValues = new List<KeyValuePair<string, string>>();

            bodyKeyValues.Add(new KeyValuePair<string, string>("transfer_code", transfer_code));
           
            bodyKeyValues.Add(new KeyValuePair<string, string>("otp", otp));

         
            var formContent = new FormUrlEncodedContent(bodyKeyValues);

            var response = await client.PostAsync("finalize_transfer", formContent);

            var json = await response.Content.ReadAsStringAsync();

            return json;
        }
    }
}
