# CarX
Требовалось провести осмотр проекта: перепроектировать, оптимизировать, исправить найденные недочеты.
Реализовать четкую, понятную, простую архитектуру, без лишних нагромаждений, в соответствии с принципами SOLID;
В первую очередь изменил структуру проекта.
![image](https://user-images.githubusercontent.com/62421378/196540150-90e74444-f15c-4671-9cda-8eba7102e77d.png)
Затем принялся уже работать с функционалом башней, чтобы уже потом их оптимизировать (изменяя также стректуру)
Для начала сделал класс Tower, в котором реализованы 2 метода IsAcquireTarget()(находит цель по коллайдеру, чтобы не искать её по всей сцене для оптимизации) и 
IsTargetTracked()(для контроля дальности этого объекта в случае если он выйдет из ренжа башни)
![image](https://user-images.githubusercontent.com/62421378/196541032-532c361a-2954-4f26-9c94-a98cc9764925.png)
Затем в двух уже имеющихся SimpleTower и CannonTower мы будем наследоваться от Tower (сделал так чтобы сократить повтор кода
Больше всего времени потратил на Cannon Tower и сделал 2 режима стрельбы параболическая траектория(мортира) и обычная, но плавное прицеливание пока недоделал.
![image](https://user-images.githubusercontent.com/62421378/196542558-7c7602cd-b706-49d2-9b0d-855bec1ca363.png)
Использовалась стрельба на упреждение
ДЛя дальнейшей оптимизации исправил постаянное создание и удаление объектов, заменив на пул(класс PoolMono) единожды созданных объектов, которые вместо уничтожения
становятся неактивными и переносятся на стартовую позицию, чтобы потом опять активироваться и повторить цикл.
Такой пул я использовал как для снарядов, так и для монстров.
![image](https://user-images.githubusercontent.com/62421378/196543501-2db54909-4a1a-4a8f-b91f-ec9240d3b359.png)
