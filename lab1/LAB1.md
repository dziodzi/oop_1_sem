﻿# ЛАБОРАТОРНАЯ РАБОТА №1
___(дедлайн 29.09.2024)___

---
### Цель: 
#### Разработать Симулятор гонок
Соревноваться могут несколько видов транспортных средств, а именно:
* Ступа Бабы Яги;
* Метла;
* Сапоги-скороходы;
* Карета-тыква;
* Ковер-самолет;
* Избушка на курьих ножках;
* Кентавр;
* Летучий корабль.

### Задача: 
* Cпроектировать классовую модель видов транспорта, и реализовать симуляцию разных видов гонок.
* Описать абстракции для транспортных средств, а также сделать описание модели взаимодействия с симулятором гонок, который должен уметь:
  1. Создавать гонку с заданной дистанцией;
  2. Регистрировать транспортные средства на гонку, исходя из вида гонки;
  3. Запускать гонку и определять победителя.
### Выполнение:
  Все виды транспортных делятся на две абстракции: _ВОЗДУШНЫЕ_ и _НАЗЕМНЫЕ_.
  
Наземные виды транспорта имеют следующие характеристики:
  * скорость движения (в условных единицах);
  * время движения до необходимого отдыха (в условных единицах);
  * длительность отдыха, которая зависит от порядкового номера остановки (условных единицах).
  
Воздушные виды транспорта характеризуются:
  * скоростью движения (в условных единицах);
  * коэффициентом ускорения (задается формулой, зависит от расстояния).
  
Необходимо придумать численные значения характеристик для каждого вида транспорта, согласно абстракции, к которой они относятся.

Гонки бывают трех видов:
  * только для наземного транспорта
  * только для воздушного транспорта
  * для всех типов транспортных средств
  > (!) На гонку для наземного транспорта нельзя зарегистрировать воздушное транспортное средство и наоборот.

### Общие пожелания по реализации:
* Разработанные решения должны соблюдать принципы SOLID (например, при проверке типов транспорта, допущенных к гонке, не нужно привязываться к конкретному ТС).
* При попытке добавить неподходящее ТС - выдавать исключение, гонка не должна запускаться.
* Формулы для характеристик ТС должны быть разными (у одного ТС это может быть константное значение, у другого линейная зависимость, а у третьего, например, экспоненциальная и т.д.)
* Предоставить пользователю возможность выбрать тип гонки и её дистанцию, ТС, которые в ней будут участвовать и запустить гонку.


---
### Усложнение задания (дополнительные баллы):
* Необходимо добавить погодные условия, где каждая погода так или иначе влияет на типы транспорта