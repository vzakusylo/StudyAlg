using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenericCollections
{
    [TestClass]
    public class Solution
    {
        [TestMethod]
        public void Main()
        {
            SortedList<int, string> sl = new SortedList<int, string>(); // поддерживает массив элементов, которые хранятся отсортированными
            SortedDictionary<int, string> sd = new SortedDictionary<int, string>();// применяет структуру красно черного дерева.

                        IEnumerable ienumberable = Enumerable.Range(1, 2);
            IEnumerable<int> ienumerablet = Enumerable.Range(1, 2);
            ICollection icollection = new List<int>() { 1, 2 };
            ICollection<int> icollectiont = new List<int>() { 1, 2 };
            IList<int> ilistt = new List<int>() { 1, 2 };
            ISet<int> isett = new HashSet<int>(); isett.Add(1);           
            IDictionary<int, string> idictionarytkeytvalue = new Dictionary<int, string>() { { 1, "1" }, { 2, "2" } };
            ICollection<KeyValuePair<int, string>> icollectionkeyvaluepairtkeytvalue = new List<KeyValuePair<int, string>>(); icollectionkeyvaluepairtkeytvalue.Add(new KeyValuePair<int, string>(1, "1"));
            HashSet<string> hashsett = new HashSet<string>(); hashsett.Add("str");
            SortedSet<string> sortedsett = new SortedSet<string>(); sortedsett.Add("str");
            LinkedList<int> linkedListt = new LinkedList<int>(); linkedListt.AddFirst(1);
            System.ComponentModel.BindingList<int> bindingList = new System.ComponentModel.BindingList<int>(); bindingList.Add(1); bindingList.AddingNew += BindingList_AddingNew; bindingList.ListChanged += BindingList_ListChanged;
            ObservableCollection<int> ibservable = new ObservableCollection<int>(); ibservable.Add(1); 
            KeyedCollection<int, string> keyedcollectiontkeytvalue = null; //keyedcollectiontkeytvalue.GetKeyForItem();
            ConcurrentDictionary<int, string> conDict = new ConcurrentDictionary<int, string>();
            IProducerConsumerCollection<int> pcc = null;
            BlockingCollection<int> bct = new BlockingCollection<int>(); bct.Add(1);
            ConcurrentBag<int> cbi = new ConcurrentBag<int>(); cbi.Add(1);
            ConcurrentQueue<int> cqi = new ConcurrentQueue<int>();
            ConcurrentStack<int> csi = new ConcurrentStack<int>();
            IReadOnlyCollection<int> roci = new List<int>().AsReadOnly();
        }

        private void BindingList_ListChanged(object sender, System.ComponentModel.ListChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BindingList_AddingNew(object sender, System.ComponentModel.AddingNewEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}

//SortedList<TKey, TValue> и SortedDictionary<TKey, TValue>
//Случайный наблюдатель может допустить, что класс по имени SortedList<,> должен был
//быть списком, но это не так.Оба эти типа в действительности являются словарями, и ни один
//из них не реализует интерфейс IList<T>.Возможно, имена ListBackedSortedDictionary
//и TreeBackedSortedDictionary были бы более информативными, но теперь уже слишком
//поздно что-то менять.
//У этих двух классов есть много общего: для сравнения ключей они оба используют интерфейс

//IComparer<TKey>, а не IEqualityComparer<TKey>, и оба поддерживают ключи в отсорти-
//рованном виде на основе данного сравнения. Оба класса обеспечивают производительность O(log

//n) при поиске значений, фактически выполняя двоичный поиск. Но их внутренние структуры

//данных серьезно отличаются: SortedList<,> поддерживает массив элементов, которые хранят-
//ся отсортированными, тогда как SortedDictionary<,> применяет структуру красно-черного

//дерева (кратко описано в Википедии по адресу http://ru.wikipedia.оrg/wiki/Красно-
//черное_дерево). Это приводит к значительным отличиям при вставке и удалении, а также

//в эффективности использования памяти.Если вы создаете словарь с применением в основном
//отсортированных данных, экземпляр SortedList<,> будет заполняться эффективно.Если вы

//представите шаги, предпринимаемые для обеспечения отсортированного вида List<T>, то уви-
//дите, что добавление одиночного элемента в конец списка не требует больших затрат (O(1),

//если проигнорировать расширение), в то время как добавление элементов произвольным обра-
//зом является дорогостоящим, поскольку вовлекает копирование существующих элементов(О(n) в

//худшем случае). Добавление элементов к сбалансированному дереву в SortedDictionary<,>

//всегда характеризуется довольно небольшими затратами(сложность O(log n)), но предусматрива-
//ет наличие в куче отдельного узла дерева для каждого элемента, приводя к большим накладным

//расходам и фрагментации памяти, чем структуры в виде массивов элементов “ключ/значение” в
//SortedList<,>.

//Обе коллекции предоставляют доступ к своим ключам и значениям как к отдельным коллек-
//циям, и в обоих случаях возвращаемые коллекции являются актуальными, отражая изменения в

//словарях. Однако коллекция в SortedList<,> реализует интерфейс IList<T>, так что возмо-
//жен доступ к элементам по индексу в форме отсортированного ключа, если это действительно

//необходимо.
//Я не хотел бы вас отпугнуть всеми этими разговорами о сложности.Если только вы не имеете
//дело с очень большим объемом данных, то, скорее всего, особо переживать по поводу того, какую
//реализацию использовать, не придется. Если наличие огромного количества элементов в словаре
//вполне вероятно, то вы должны тщательно проанализировать характеристики производительности
//обеих коллекций, чтобы выяснить, какая из них более приемлема.
