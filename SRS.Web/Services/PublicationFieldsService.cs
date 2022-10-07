using System;
using System.Linq;
using SRS.Domain.Enums;
using SRS.Web.Enums;

namespace SRS.Web.Services
{
    public static class PublicationFieldsService
    {
        public static FieldInfo[] GetAvailablePublicationTypes(PublicationField publicationField)
        {
            switch (publicationField)
            {
                case PublicationField.Name:
                    var fieldInfos = Enum.GetValues(typeof(PublicationType)).Cast<int>().ToDictionary(x => x, x => new FieldInfo { Type = x });
                    var articleName = "Назва статті";
                    fieldInfos[(int)PublicationType.Стаття_В_Виданнях_які_мають_імпакт_фактор].Name = articleName;
                    fieldInfos[(int)PublicationType.Стаття_В_Інших_Виданнях_які_включені_до_міжнародних_наукометричних_баз_даних].Name = articleName;
                    fieldInfos[(int)PublicationType.Стаття_В_Інших_Закордонних_Виданнях].Name = articleName;
                    fieldInfos[(int)PublicationType.Стаття_В_Фахових_Виданнях_України].Name = articleName;
                    fieldInfos[(int)PublicationType.Стаття_В_Інших_Виданнях_України].Name = articleName;
                    return fieldInfos.Values.ToArray();
                case PublicationField.MainAuthor:
                case PublicationField.Link:
                    return new FieldInfo[]
                    {
                        new FieldInfo { Type = (int)PublicationType.Монографія_У_Закордонному_Видавництві },
                        new FieldInfo { Type = (int)PublicationType.Монографія_У_Вітчизняному_Видавництві },
                        new FieldInfo { Type = (int)PublicationType.Підручник },
                        new FieldInfo { Type = (int)PublicationType.Навчальний_Посібник },
                        new FieldInfo { Type = (int)PublicationType.Інше_Наукове_Видання },
                        new FieldInfo { Type = (int)PublicationType.Розділ_монографії },
                        new FieldInfo { Type = (int)PublicationType.Стаття_В_Інших_Виданнях_які_включені_до_міжнародних_наукометричних_баз_даних },
                        new FieldInfo { Type = (int)PublicationType.Стаття_В_Інших_Закордонних_Виданнях },
                        new FieldInfo { Type = (int)PublicationType.Стаття_В_Виданнях_які_мають_імпакт_фактор },
                        new FieldInfo { Type = (int)PublicationType.Стаття_В_Фахових_Виданнях_України },
                        new FieldInfo { Type = (int)PublicationType.Стаття_В_Інших_Виданнях_України },
                        new FieldInfo { Type = (int)PublicationType.Тези_Доповіді_На_Міжнародній_Конференції },
                        new FieldInfo { Type = (int)PublicationType.Тези_Доповіді_На_Вітчизняній_Конференції }
                    };
                case PublicationField.Place:
                case PublicationField.Edition:
                    return new FieldInfo[]
                    {
                        new FieldInfo { Type = (int)PublicationType.Монографія_У_Закордонному_Видавництві },
                        new FieldInfo { Type = (int)PublicationType.Монографія_У_Вітчизняному_Видавництві },
                        new FieldInfo { Type = (int)PublicationType.Підручник },
                        new FieldInfo { Type = (int)PublicationType.Навчальний_Посібник },
                        new FieldInfo { Type = (int)PublicationType.Інше_Наукове_Видання },
                        new FieldInfo { Type = (int)PublicationType.Розділ_монографії },
                        new FieldInfo { Type = (int)PublicationType.Тези_Доповіді_На_Міжнародній_Конференції },
                        new FieldInfo { Type = (int)PublicationType.Тези_Доповіді_На_Вітчизняній_Конференції }
                    };
                case PublicationField.Date:
                    return new FieldInfo[]
                    {
                        new FieldInfo { Type = (int)PublicationType.Монографія_У_Закордонному_Видавництві, Name = "Рік видання" },
                        new FieldInfo { Type = (int)PublicationType.Монографія_У_Вітчизняному_Видавництві, Name = "Рік видання" },
                        new FieldInfo { Type = (int)PublicationType.Підручник, Name = "Рік видання" },
                        new FieldInfo { Type = (int)PublicationType.Навчальний_Посібник, Name = "Рік видання" },
                        new FieldInfo { Type = (int)PublicationType.Інше_Наукове_Видання, Name = "Рік видання" },
                        new FieldInfo { Type = (int)PublicationType.Розділ_монографії, Name = "Рік видання" },
                        new FieldInfo { Type = (int)PublicationType.Стаття_В_Інших_Виданнях_які_включені_до_міжнародних_наукометричних_баз_даних },
                        new FieldInfo { Type = (int)PublicationType.Стаття_В_Інших_Закордонних_Виданнях },
                        new FieldInfo { Type = (int)PublicationType.Стаття_В_Виданнях_які_мають_імпакт_фактор },
                        new FieldInfo { Type = (int)PublicationType.Стаття_В_Фахових_Виданнях_України },
                        new FieldInfo { Type = (int)PublicationType.Стаття_В_Інших_Виданнях_України },
                        new FieldInfo { Type = (int)PublicationType.Тези_Доповіді_На_Міжнародній_Конференції },
                        new FieldInfo { Type = (int)PublicationType.Тези_Доповіді_На_Вітчизняній_Конференції }
                    };
                case PublicationField.Tome:
                    return new FieldInfo[]
                    {
                        new FieldInfo { Type = (int)PublicationType.Монографія_У_Закордонному_Видавництві },
                        new FieldInfo { Type = (int)PublicationType.Монографія_У_Вітчизняному_Видавництві },
                        new FieldInfo { Type = (int)PublicationType.Підручник },
                        new FieldInfo { Type = (int)PublicationType.Навчальний_Посібник },
                        new FieldInfo { Type = (int)PublicationType.Інше_Наукове_Видання },
                        new FieldInfo { Type = (int)PublicationType.Розділ_монографії },
                        new FieldInfo { Type = (int)PublicationType.Стаття_В_Інших_Виданнях_які_включені_до_міжнародних_наукометричних_баз_даних },
                        new FieldInfo { Type = (int)PublicationType.Стаття_В_Інших_Закордонних_Виданнях },
                        new FieldInfo { Type = (int)PublicationType.Стаття_В_Виданнях_які_мають_імпакт_фактор },
                        new FieldInfo { Type = (int)PublicationType.Стаття_В_Фахових_Виданнях_України },
                        new FieldInfo { Type = (int)PublicationType.Стаття_В_Інших_Виданнях_України }
                    };
                case PublicationField.NumberOfPages:
                    return new FieldInfo[]
                    {
                        new FieldInfo { Type = (int)PublicationType.Монографія_У_Закордонному_Видавництві },
                        new FieldInfo { Type = (int)PublicationType.Монографія_У_Вітчизняному_Видавництві },
                        new FieldInfo { Type = (int)PublicationType.Підручник },
                        new FieldInfo { Type = (int)PublicationType.Навчальний_Посібник },
                        new FieldInfo { Type = (int)PublicationType.Інше_Наукове_Видання }
                    };
                case PublicationField.ISBN:
                    return new FieldInfo[]
                    {
                        new FieldInfo { Type = (int)PublicationType.Монографія_У_Закордонному_Видавництві },
                        new FieldInfo { Type = (int)PublicationType.Монографія_У_Вітчизняному_Видавництві },
                        new FieldInfo { Type = (int)PublicationType.Підручник },
                        new FieldInfo { Type = (int)PublicationType.Навчальний_Посібник },
                        new FieldInfo { Type = (int)PublicationType.Інше_Наукове_Видання },
                        new FieldInfo { Type = (int)PublicationType.Розділ_монографії }
                    };
                case PublicationField.PageFrom:
                case PublicationField.PageTo:
                    return new FieldInfo[]
                    {
                        new FieldInfo { Type = (int)PublicationType.Розділ_монографії },
                        new FieldInfo { Type = (int)PublicationType.Стаття_В_Інших_Виданнях_які_включені_до_міжнародних_наукометричних_баз_даних },
                        new FieldInfo { Type = (int)PublicationType.Стаття_В_Інших_Закордонних_Виданнях },
                        new FieldInfo { Type = (int)PublicationType.Стаття_В_Виданнях_які_мають_імпакт_фактор },
                        new FieldInfo { Type = (int)PublicationType.Стаття_В_Фахових_Виданнях_України },
                        new FieldInfo { Type = (int)PublicationType.Стаття_В_Інших_Виданнях_України },
                        new FieldInfo { Type = (int)PublicationType.Тези_Доповіді_На_Міжнародній_Конференції },
                        new FieldInfo { Type = (int)PublicationType.Тези_Доповіді_На_Вітчизняній_Конференції }
                    };
                case PublicationField.PublicationIdentifier:
                    return new FieldInfo[]
                    {
                        new FieldInfo { Type = (int)PublicationType.Стаття_В_Інших_Виданнях_які_включені_до_міжнародних_наукометричних_баз_даних },
                        new FieldInfo { Type = (int)PublicationType.Стаття_В_Інших_Закордонних_Виданнях },
                        new FieldInfo { Type = (int)PublicationType.Стаття_В_Виданнях_які_мають_імпакт_фактор },
                        new FieldInfo { Type = (int)PublicationType.Стаття_В_Фахових_Виданнях_України },
                        new FieldInfo { Type = (int)PublicationType.Стаття_В_Інших_Виданнях_України },
                        new FieldInfo { Type = (int)PublicationType.Тези_Доповіді_На_Міжнародній_Конференції, Name = "Ідентифікатор (номер) тези" },
                        new FieldInfo { Type = (int)PublicationType.Тези_Доповіді_На_Вітчизняній_Конференції, Name = "Ідентифікатор (номер) тези" }
                    };
                case PublicationField.Journal:
                case PublicationField.Issue:
                case PublicationField.DOI:
                    return new FieldInfo[]
                    {
                        new FieldInfo { Type = (int)PublicationType.Стаття_В_Інших_Виданнях_які_включені_до_міжнародних_наукометричних_баз_даних },
                        new FieldInfo { Type = (int)PublicationType.Стаття_В_Виданнях_які_мають_імпакт_фактор },
                        new FieldInfo { Type = (int)PublicationType.Стаття_В_Фахових_Виданнях_України },
                        new FieldInfo { Type = (int)PublicationType.Стаття_В_Інших_Виданнях_України },
                        new FieldInfo { Type = (int)PublicationType.Стаття_В_Інших_Закордонних_Виданнях }
                    };
                case PublicationField.ConferenceName:
                case PublicationField.ConferenceDate:
                case PublicationField.ConferencePlace:
                case PublicationField.ConferenceCountry:
                case PublicationField.ConferenceEdition:
                    return new FieldInfo[]
                    {
                        new FieldInfo { Type = (int)PublicationType.Тези_Доповіді_На_Міжнародній_Конференції },
                        new FieldInfo { Type = (int)PublicationType.Тези_Доповіді_На_Вітчизняній_Конференції }
                    };
                case PublicationField.PublicationDate:
                case PublicationField.BulletinNumber:
                    return new FieldInfo[]
                    {
                        new FieldInfo { Type = (int)PublicationType.Патент }
                    };
                case PublicationField.ApplicationDate:
                case PublicationField.ApplicationNumber:
                case PublicationField.ApplicationOwner:
                    return new FieldInfo[]
                    {
                        new FieldInfo { Type = (int)PublicationType.Заявка_на_винахід },
                        new FieldInfo { Type = (int)PublicationType.Патент }
                    };

                default: return new FieldInfo[0];
            }
        }
    }
}