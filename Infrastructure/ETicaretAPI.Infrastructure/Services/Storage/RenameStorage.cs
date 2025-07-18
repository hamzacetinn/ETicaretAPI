﻿using ETicaretAPI.Infrastructure.Operations;

namespace ETicaretAPI.Infrastructure.Services.Storage.Storage
{
    public class RenameStorage
    {
        protected delegate bool HasFile(string pathOrContainer, string fileName);
        protected async Task<string> FileRenameAsync(string pathOrContainer, string fileName, HasFile hasFileMethhod, bool first = true)
        {
            string newFileName = await Task.Run(async () =>   //Asenkron bir fomksiyon üzerinden gerçekleştirmemizi sağlar.
            {
                string extencion = Path.GetExtension(fileName);
                string newFileName = string.Empty;
                if (first)
                {
                    string oldName = Path.GetFileNameWithoutExtension(fileName);
                    newFileName = $"{NameOperation.CharacterRegulatory(oldName)}{extencion}"; // oluşturduğumuz nameoperation sınıfı ile istenmeyen karakterleri
                                                                                              // düzeltmiş oluyoruz ve uzantısıyla birlikte birleşerek istenmeyen
                                                                                              // karakterlerden kurtulmuş yeni haline kavuşmuş olduk
                }
                else
                {
                    newFileName = fileName;
                    int indexNo1 = newFileName.IndexOf("-"); //aranan indexx bulunmazsa -1 döndürür.
                    if (indexNo1 == -1)
                    {
                        newFileName = $"{Path.GetFileNameWithoutExtension(fileName)}-2{extencion}";
                    }
                    else
                    {
                        int lastIndex = 0;
                        while (true)
                        {
                            #region 

                            //lastIndex = newFileName.IndexOf("-", indexNo1 + 1);
                            //indexNo1 = lastIndex;
                            #endregion
                            lastIndex = indexNo1;
                            indexNo1 = newFileName.IndexOf("-", indexNo1 + 1);
                            if (indexNo1 == -1)
                                indexNo1 = lastIndex;
                            break;
                        }

                        int indexNo2 = fileName.IndexOf(".");
                        string fileNo = newFileName.Substring(indexNo1 + 1, (indexNo2 - indexNo1) - 1);

                        if (int.TryParse(fileNo, out int _fileNo))
                        {
                            _fileNo++;
                            newFileName = newFileName.Remove(indexNo1 + 1, (indexNo2 - indexNo1) - 1)
                                                     .Insert(indexNo1, _fileNo.ToString());
                        }
                        else
                        {
                            newFileName = $"{Path.GetFileNameWithoutExtension(fileName)}-2{extencion}";

                        }
                    }
                }
                //if (File.Exists($"{path}\\{newFileName}"))
                if (hasFileMethhod(pathOrContainer, newFileName))
                    return await FileRenameAsync(pathOrContainer, newFileName, hasFileMethhod, false);
                else
                    return newFileName;
            });
            return newFileName;
        }
    }
}
