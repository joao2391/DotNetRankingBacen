# DotNetRankingBacen [![Nuget](https://img.shields.io/nuget/v/DotNetRankingBacen)](https://www.nuget.org/packages/DotNetRankingBacen/) ![Nuget](https://img.shields.io/nuget/dt/DotNetRankingBacen)

DotNetRankingBacen is a .Net library that helps you to get Top 03 banking institutions from claim's list of BACEN.

## Notes
Version 1.0.0:

BETA

## Installation

Use the package manager to install.

```bash
Install-Package DotNetRankingBacen  -Version 1.0.0
```

## Usage

```C#
services.<ChooseYours><IHttpClientWrapper, HttpClientWrapper>();
services.<ChooseYours><IRankingBacen, RankingBacen>();

```

### Features
You will get an object with an object's array of size 03
```C#
var bancosFinanceiras = GetTop03BancosEFinanceirasAsync();
```
 
```C#
var demaisBancosFinancerias = GetTop03DemaisBancosEFinanceirasAsync();
```
```C
var reclamacoes = GetTop03ReclamacoesAsync();
```
```C#
var administradorasConsorcio = GetTop03AdmConsorcioAsync();
```


## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License
[MIT](https://choosealicense.com/licenses/mit/)
