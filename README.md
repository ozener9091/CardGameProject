# Card Game

## Описание

Данная карточная игра реализована на C# и позволяет игроку сражаться с ботом, используя карты существ и заклинаний. Игра включает в себя концепцию колоды, руки и стола, на которых располагаются карты. Также реализована функциональность сохранения и загрузки состояния игры.

## Структура проекта

Проект состоит из следующих основных компонентов:

- **Card**: Абстрактный класс, от которого наследуются все типы карт.
- **Creature**: Класс, представляющий существ, которые могут атаковать противников.
- **Spell**: Классы, представляющие заклинания, которые могут наносить урон или лечить существ.
- **CardInGame**: Класс, управляющий колодой, рукой и столом.
- **GameState**: Класс, представляющий состояние игры, включая колоду, руку, стол и роль игрока.
- **Save**: Статический класс, отвечающий за сохранение состояния игры.
- **Load**: Статический класс, отвечающий за загрузку состояния игры.
- **Enums**: Перечисления для определения типов карт и ролей (игрок или противник).
- **Тесты**: Класс `CardGameTestClass`, содержащий юнит-тесты для проверки функциональности игры.

## Функциональные возможности

- Создание и управление колодой карт.
- Выкладывание существ на стол.
- Атака существ противника.
- Использование заклинаний для нанесения урона или лечения.
- Сохранение и загрузка состояния игры.
- Проверка состояния существ (жив ли игрок, мертв ли враг).
- Условия победы: если главное существо игрока умирает, игра заканчивается.

## Как играть

1. **Запустите проект**: Откройте проект в Visual Studio и запустите его.
2. **Начало игры**: Игра создаст колоду и раздаст 4 карты игроку.
3. **Взаимодействие**: Игрок можнь выкладывать карты на стол и использовать их для атаки противников или лечения своих существ.
4. **Сохранение и загрузка**: Каждый ход происходит автосохранение, загрузить недоигронную игру можно при следующем запуске программы.

## Как использовать

### 1. Установка

Убедитесь, что у вас установлен .NET Framework 4.7.2 или новее.

### 2. Клонирование репозитория

Сначала клонируйте репозиторий на свой компьютер:

```bash git clone https://github.com/USERNAME/CardGame.git ```

### 3. Открытие проекта
Откройте проект в Visual Studio, выберите нужный проект и нажмите "Запустить".

### 4. Игра
Следуйте инструкциям, которые отображаются в консоли, и играйте с другим игроком, используя карты.

### 5. Сохранение и загрузка
Для сохранения текущего состояния игры используйте метод Save.SaveGame(player, enemy).
Для загрузки сохраненного состояния используйте метод Load.LoadGame(player, enemy).
Пример использования:
```
// Создание новых объектов игры
CardInGame player = new CardInGame(TypeOfRole.Player);
CardInGame enemy = new CardInGame(TypeOfRole.Enemy);

// Вывести информацию о руке игрока
player.PrintHand();
// Выложить карту на стол
player.AddOnBoard(0);

// Использовать карту
player.HandList[0].UseCard(enemy.BoardList[0]);

// Сохранить состояние игры
Save.SaveGame(player, enemy);

// Загрузить состояние игры
Load.LoadGame(player, enemy);
```
### 6. Тестирование
Для тестирования функциональности игры используются юнит-тесты, реализованные с помощью MSTest. 
Пример тестов включает проверку:
 - Корректности создания карт.
 - Применения заклинаний.
 - Проверки состояния существ (живы ли они или мертвы).
