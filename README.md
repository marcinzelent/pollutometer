# Pollutometer

A system for monitoring air pollution at Roskilde train station. Mandatory
school project on 3rd semester of Computer Science course at Erhvervsakademi
Sjælland.

## Preview

![Preview](/.github/preview.gif)

## Features

- current air pollution display, i.e. concentration of SO2, NO2 and CO
- calculation of current [air quality index](https://en.wikipedia.org/wiki/Air_quality_index)
- tables showing all gathered data and data from last week
- graphs visualizing the changes in gas concentration
- train schedule for Roskilde St. with AQI measurement corresponding to train
departure
- warning e-mails, sent when air pollution could be harmful

## Architecture

![Architecture](/.github/architecture.png)

The system's architecture is built according to the project requirements and
consists of several components:
- Website built with PHP, Symphony and Chart.js, serving as a front-end for the
users and allowing them to see the gathered data
- Microsoft SQL server database holding all the readings
- RESTful Web API created with ASP.NET which acts as a bridge between the
website and the database by serving the data in JSON format
- Raspberry Pi, used for gathering the data and posting it to the Web API
- OpenAQ API, source of pollution readings (replacement for RPi sensor)
- Rejseplanen API, to get train departures at Roskilde St.


## Authors
- [Jeppe Holm](https://github.com/Shadify)
- [Adnan Ünal](https://github.com/marwolaethblack)
- [Jiří Vrbas](https://linkedin.com/in/jiří-vrbas-a2286514a)
- [Marcin Zelent](https://github.com/marcinzelent)
