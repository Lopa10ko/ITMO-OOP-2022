# Лабы по ООП


## Лаба 0. Isu
```
Цель: ознакомление с C# и базовыми механизмами ООП.
Дано: описанные базовые сущности
Требования: реализовать недостающие методы
            написать тесты, которые бы проверили корректность работы 
```
```cs
|----------------------|
|       Student()      |   
|----------------------|
| -idIsu:ulong         |     Номер ИСУ
| -name:string         |     ФИО
| -groupName:GroupName |     Группа в формате [A-Z]****
| -course:int          |     Какой курс
```
```cs
|------------------------|
|         Group()        |   
|------------------------|
| -GroupMaxQuantity:int  |     Максимальная вместимость группы
| -groupName:GroupName   |     Группа в формате [A-Z]*[1-4]** в зав-ти от курса
| -students:List<Student> |     Коллекция из студентов группы
```

Студент может изменять свою группу, при этом он не может числиться в двух группах одновременно. При переводе студента необходимо обратиться к экземпляру его группы и удалить объект студента из списка группы, после этого отправить этот объект в список новой группы. \
В классе группы должен появится метод <code>RemoveStudent</code> + должны уметь изменять информацию о принадлежности к группе в экземпляре студента. \
Перед тем, как переводить студента, необходимо проверить, состоит ли (ещё, на момент замены) он в начальной группе и состоит ли (уже, на момент замены) в заменяемой группе -> пробрасывать исключения, если что-то пойдет не по плану.