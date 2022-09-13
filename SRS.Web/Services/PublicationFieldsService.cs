using SRS.Domain.Enums;
using SRS.Web.Enums;

namespace SRS.Web.Services
{
    public static class PublicationFieldsService
    {
        public static int[] GetAvailablePublicationTypes(PublicationField publicationField)
        {
            switch (publicationField)
            {
                case PublicationField.MainAuthor:
                case PublicationField.Link:
                    return new int[]
                    {
                        (int)PublicationType.Монографія,
                        (int)PublicationType.Підручник,
                        (int)PublicationType.Навчальний_Посібник,
                        (int)PublicationType.Інше_Наукове_Видання,
                        (int)PublicationType.Розділ_монографії,
                        (int)PublicationType.Стаття_В_Інших_Виданнях_які_включені_до_міжнародних_наукометричних_баз_даних,
                        (int)PublicationType.Стаття_В_Інших_Закордонних_Виданнях,
                        (int)PublicationType.Стаття_В_Виданнях_які_мають_імпакт_фактор,
                        (int)PublicationType.Стаття_В_Фахових_Виданнях_України,
                        (int)PublicationType.Стаття_В_Інших_Виданнях_України,
                        (int)PublicationType.Тези_Доповіді_На_Міжнародній_Конференції,
                        (int)PublicationType.Тези_Доповіді_На_Вітчизняній_Конференції
                    };
                case PublicationField.Place:
                case PublicationField.Edition:
                    return new int[]
                    {
                        (int)PublicationType.Монографія,
                        (int)PublicationType.Підручник,
                        (int)PublicationType.Навчальний_Посібник,
                        (int)PublicationType.Інше_Наукове_Видання,
                        (int)PublicationType.Розділ_монографії,
                        (int)PublicationType.Тези_Доповіді_На_Міжнародній_Конференції,
                        (int)PublicationType.Тези_Доповіді_На_Вітчизняній_Конференції
                    };
                case PublicationField.Date:
                    return new int[]
                    {
                        (int)PublicationType.Монографія,
                        (int)PublicationType.Підручник,
                        (int)PublicationType.Навчальний_Посібник,
                        (int)PublicationType.Інше_Наукове_Видання,
                        (int)PublicationType.Розділ_монографії,
                        (int)PublicationType.Стаття_В_Інших_Виданнях_які_включені_до_міжнародних_наукометричних_баз_даних,
                        (int)PublicationType.Стаття_В_Інших_Закордонних_Виданнях,
                        (int)PublicationType.Стаття_В_Виданнях_які_мають_імпакт_фактор,
                        (int)PublicationType.Стаття_В_Фахових_Виданнях_України,
                        (int)PublicationType.Стаття_В_Інших_Виданнях_України,
                        (int)PublicationType.Тези_Доповіді_На_Міжнародній_Конференції,
                        (int)PublicationType.Тези_Доповіді_На_Вітчизняній_Конференції,
                    };
                case PublicationField.Tome:
                    return new int[]
                    {
                        (int)PublicationType.Монографія,
                        (int)PublicationType.Підручник,
                        (int)PublicationType.Навчальний_Посібник,
                        (int)PublicationType.Інше_Наукове_Видання,
                        (int)PublicationType.Розділ_монографії,
                        (int)PublicationType.Стаття_В_Інших_Виданнях_які_включені_до_міжнародних_наукометричних_баз_даних,
                        (int)PublicationType.Стаття_В_Інших_Закордонних_Виданнях,
                        (int)PublicationType.Стаття_В_Виданнях_які_мають_імпакт_фактор,
                        (int)PublicationType.Стаття_В_Фахових_Виданнях_України,
                        (int)PublicationType.Стаття_В_Інших_Виданнях_України
                    };
                case PublicationField.NumberOfPages:
                    return new int[]
                    {
                        (int)PublicationType.Монографія,
                        (int)PublicationType.Підручник,
                        (int)PublicationType.Навчальний_Посібник,
                        (int)PublicationType.Інше_Наукове_Видання
                    };
                case PublicationField.ISBN:
                    return new int[]
                    {
                        (int)PublicationType.Монографія,
                        (int)PublicationType.Підручник,
                        (int)PublicationType.Навчальний_Посібник,
                        (int)PublicationType.Інше_Наукове_Видання,
                        (int)PublicationType.Розділ_монографії
                    };
                case PublicationField.PageFrom:
                case PublicationField.PageTo:
                    return new int[]
                    {
                        (int)PublicationType.Розділ_монографії,
                        (int)PublicationType.Стаття_В_Інших_Виданнях_які_включені_до_міжнародних_наукометричних_баз_даних,
                        (int)PublicationType.Стаття_В_Інших_Закордонних_Виданнях,
                        (int)PublicationType.Стаття_В_Виданнях_які_мають_імпакт_фактор,
                        (int)PublicationType.Стаття_В_Фахових_Виданнях_України,
                        (int)PublicationType.Стаття_В_Інших_Виданнях_України,
                        (int)PublicationType.Тези_Доповіді_На_Міжнародній_Конференції,
                        (int)PublicationType.Тези_Доповіді_На_Вітчизняній_Конференції
                    };
                case PublicationField.Journal:
                case PublicationField.Issue:
                case PublicationField.DOI:
                    return new int[]
                    {
                        (int)PublicationType.Стаття_В_Інших_Виданнях_які_включені_до_міжнародних_наукометричних_баз_даних,
                        (int)PublicationType.Стаття_В_Виданнях_які_мають_імпакт_фактор,
                        (int)PublicationType.Стаття_В_Фахових_Виданнях_України,
                        (int)PublicationType.Стаття_В_Інших_Виданнях_України
                    };
                case PublicationField.ConferenceName:
                case PublicationField.ConferenceDate:
                case PublicationField.ConferencePlace:
                case PublicationField.ConferenceCountry:
                case PublicationField.ConferenceIssue:
                    return new int[]
                    {
                        (int)PublicationType.Тези_Доповіді_На_Міжнародній_Конференції,
                        (int)PublicationType.Тези_Доповіді_На_Вітчизняній_Конференції
                    };
                case PublicationField.PublicationDate:
                case PublicationField.BulletinNumber:
                    return new int[]
                    {
                        (int)PublicationType.Патент
                    };
                case PublicationField.ApplicationDate:
                case PublicationField.ApplicationNumber:
                case PublicationField.ApplicationOwner:
                    return new int[]
                    {
                        (int)PublicationType.Заявка_на_винахід,
                        (int)PublicationType.Патент
                    };

                default: return new int[0];
            }
        }
    }
}