﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using NetTelegramBotApi.Types;

namespace NetTelegramBotApi.Requests
{
    /// <summary>
    /// Use this method to send text messages. On success, the sent Message is returned.
    /// </summary>
    public class SendMessage : RequestBase<Message>
    {
        public SendMessage(long chatId, string text) 
            : base("sendMessage")
        {
            this.ChatId = chatId;
            this.Text = text;
        }

        /// <summary>
        /// Unique identifier for the message recipient — User or GroupChat id
        /// </summary>
        public long ChatId { get; set; }

        /// <summary>
        /// Text of the message to be sent
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Send "Markdown", if you want Telegram apps to show bold, italic and inline URLs in your bot's message. 
        /// For the moment, only Telegram for Android supports this.
        /// </summary>
        public ParseModeEnum ParseMode { get; set; }

        /// <summary>
        /// Optional. Disables link previews for links in this message
        /// </summary>
        public bool? DisableWebPagePreview { get; set; }

        /// <summary>
        /// Optional. If the message is a reply, ID of the original message
        /// </summary>
        public long? ReplyToMessageId { get; set; }

        /// <summary>
        /// Optional. Additional interface options. A JSON-serialized object for a custom reply keyboard, 
        /// instructions to hide keyboard or to force a reply from the user.
        /// </summary>
        public ReplyMarkupBase ReplyMarkup { get; set; }

        public override HttpContent CreateHttpContent()
        {
            var dic = new Dictionary<string, string>();

            dic.Add("chat_id", ChatId.ToString());
            dic.Add("text", Text);
            if (ParseMode == ParseModeEnum.Markdown)
            {
                dic.Add("parse_mode", "Markdown");
            }
            if (DisableWebPagePreview.HasValue)
            {
                dic.Add("disable_web_page_preview", DisableWebPagePreview.Value.ToString());
            }
            if (ReplyToMessageId.HasValue)
            {
                dic.Add("reply_to_message_id", ReplyToMessageId.Value.ToString());
            }
            if (ReplyMarkup != null)
            {
                dic.Add("reply_markup", JsonSerialize(ReplyMarkup));
            }

            return new FormUrlEncodedContent(dic);
        }

        public enum ParseModeEnum
        {
            None,
            Markdown
        }
    }
}
