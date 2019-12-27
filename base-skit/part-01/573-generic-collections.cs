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
            SortedList<int, string> sl = new SortedList<int, string>(); // ������������ ������ ���������, ������� �������� ����������������
            SortedDictionary<int, string> sd = new SortedDictionary<int, string>();// ��������� ��������� ������ ������� ������.

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

//SortedList<TKey, TValue> � SortedDictionary<TKey, TValue>
//��������� ����������� ����� ���������, ��� ����� �� ����� SortedList<,> ������ ���
//���� �������, �� ��� �� ���.��� ��� ���� � ���������������� �������� ���������, � �� ����
//�� ��� �� ��������� ��������� IList<T>.��������, ����� ListBackedSortedDictionary
//� TreeBackedSortedDictionary ���� �� ����� ��������������, �� ������ ��� �������
//������ ���-�� ������.
//� ���� ���� ������� ���� ����� ������: ��� ��������� ������ ��� ��� ���������� ���������

//IComparer<TKey>, � �� IEqualityComparer<TKey>, � ��� ������������ ����� � �������-
//�������� ���� �� ������ ������� ���������. ��� ������ ������������ ������������������ O(log

//n) ��� ������ ��������, ���������� �������� �������� �����. �� �� ���������� ���������

//������ �������� ����������: SortedList<,> ������������ ������ ���������, ������� ������-
//�� ����������������, ����� ��� SortedDictionary<,> ��������� ��������� ������-�������

//������ (������ ������� � ��������� �� ������ http://ru.wikipedia.�rg/wiki/������-
//������_������). ��� �������� � ������������ �������� ��� ������� � ��������, � �����

//� ������������� ������������� ������.���� �� �������� ������� � ����������� � ��������
//��������������� ������, ��������� SortedList<,> ����� ����������� ����������.���� ��

//����������� ����, ��������������� ��� ����������� ���������������� ���� List<T>, �� ���-
//����, ��� ���������� ���������� �������� � ����� ������ �� ������� ������� ������ (O(1),

//���� ��������������� ����������), � �� ����� ��� ���������� ��������� ������������ ����-
//��� �������� �������������, ��������� ��������� ����������� ������������ ���������(�(n) �

//������ ������). ���������� ��������� � ����������������� ������ � SortedDictionary<,>

//������ ��������������� �������� ���������� ���������(��������� O(log n)), �� �������������-
//�� ������� � ���� ���������� ���� ������ ��� ������� ��������, ������� � ������� ���������

//�������� � ������������ ������, ��� ��������� � ���� �������� ��������� �����/�������� �
//SortedList<,>.

//��� ��������� ������������� ������ � ����� ������ � ��������� ��� � ��������� ������-
//����, � � ����� ������� ������������ ��������� �������� �����������, ������� ��������� �

//��������. ������ ��������� � SortedList<,> ��������� ��������� IList<T>, ��� ��� �����-
//��� ������ � ��������� �� ������� � ����� ���������������� �����, ���� ��� �������������

//����������.
//� �� ����� �� ��� ��������� ����� ����� ����������� � ���������.���� ������ �� �� ������
//���� � ����� ������� ������� ������, ��, ������ �����, ����� ���������� �� ������ ����, �����
//���������� ������������, �� ��������. ���� ������� ��������� ���������� ��������� � �������
//������ ��������, �� �� ������ ��������� ���������������� �������������� ������������������
//����� ���������, ����� ��������, ����� �� ��� ����� ���������.
