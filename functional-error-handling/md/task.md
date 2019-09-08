### Task

Want to create a web api that:

- downloads a named csv of transactions from sftp server
- parses and counts amount of transactions by John
- returns 200 with json of result

---

![Task](functional-error-handling/assets/img/Task.png)

---

Things that can go wrong:

- Web server can not be allowed to access file -> 401
- File might not exist on sftp server -> 404
- File might be un-parsable -> 422
- Other error -> 500

---

![Task](functional-error-handling/assets/img/TaskPipeline.png)

---

Demo time! - Errors, data and parser

---?code=functional-error-handling/code/Downloader.Api.Shared/Data.cs&lang=cs

@[3-8](File names from api control behaviour and errors)
@[12-21](Fake csv file can be either valid or invalid)

---?code=functional-error-handling/code/Downloader.Api/Other/PersonAmount.cs&lang=cs

---?code=functional-error-handling/code/Downloader.Api/Other/FileParser.cs&lang=cs

---

Demo time! - Data, parser and errors
- Normal controller
- Result controller

