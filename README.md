# DotNetRankingBacen [![Nuget](https://img.shields.io/nuget/v/DotNetRankingBacen)](https://www.nuget.org/packages/DotNetRankingBacen/) ![Nuget](https://img.shields.io/nuget/dt/DotNetRankingBacen)

DotNetRankingBacen is a .Net library that helps you to get all informations from claim's list of BACEN (current quarter).

## Notes
Version 1.1.0:

- Added features to get all information from claim's list of BACEN.

## Installation

Use the package manager to install.

```bash
Install-Package DotNetRankingBacen  -Version 1.1.0
```

## Usage - Without Selenium

```C#
services.<ChooseYours><IHttpClientWrapper, HttpClientWrapper>();
services.<ChooseYours><IRankingBacen, RankingBacen>();

```

### Features
You will get an object with an object's array of size 03
```C#
var bancosFinanceiras = await GetTop03BancosEFinanceirasAsync();
```
 
```C#
var demaisBancosFinancerias = await GetTop03DemaisBancosEFinanceirasAsync();
```
```C#
var reclamacoes = await GetTop03ReclamacoesAsync();
```
```C#
var administradorasConsorcio = await GetTop03AdmConsorcioAsync();
```
## Usage - With Selenium
Download chromedriver.exe (version 94 or higher) from [official website](https://chromedriver.chromium.org/downloads).

After downloaded it, extract the exe to any directory.

### Features
There's a constructor with those parameters:
```C#
public RankingBacen(string chromeDriverPath, ChromeOptions driverOptions, int timeToSleep) { ... }
```

You must fill up all parameters to use all features below:
(Check how to use ChromeOptions: https://chromedriver.chromium.org/capabilities)
```C#

 var chromeDriverPath = @"path/to/chromedriver.exe";

 var chromeOptions = new ChromeOptions();
     chromeOptions.AddArgument("--headless"); //execute the chromedriver without open the browser

var timeToSleep = 2000; // time in ms to wait the page be loaded

var rankingBacen = new RankingBacen(chromeDriverPath, chromeOptions, timeToSleep);

var top10BancosEFinanceiras = rankingBacen.GetTop10BancosEFinanceiras(); // it returns the full list of Bancos e Financeiras from BACEN

var demaisBancosEFinanceiras = rankingBacen.GetDemaisBancosEFinanceiras(); // it returns the full list of Demais Bancos e Financeiras from BACEN

var todasReclamacoes = rankingBacen.GetTodasReclamacoes(); // it returns the full list of Reclamacoes from BACEN

var todasAdmnistradorasConsorcio = rankingBacen.GetTodasAdmsConsorcio(); // it returns the full list of Administradoras de Consorcio from BACEN
```

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License
[MIT](https://choosealicense.com/licenses/mit/)
