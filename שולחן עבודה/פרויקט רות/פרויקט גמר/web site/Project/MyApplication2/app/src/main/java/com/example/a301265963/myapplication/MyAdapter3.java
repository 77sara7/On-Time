package com.example.a301265963.myapplication;

import android.content.Context;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.CheckBox;
import android.widget.CompoundButton;
import android.widget.EditText;
import android.widget.NumberPicker;
import android.widget.TextView;

import com.example.a301265963.myapplication.db.MyDB;
import com.example.a301265963.myapplication.db.Orders;

import java.util.List;

/**
 * Created by 315561779 on 26/04/2017.
 */

public class MyAdapter3 extends BaseAdapter {

        List<Orders> OrderstList = null;
        Context context = null;
        EditText num=null;
        MyDB myDB=null;

        @Override
        public int getCount() {
            return OrderstList.size();
        }

        public MyAdapter3(List<Orders> OrderstList, Context context) {
            this.OrderstList = OrderstList;
            this.context = context;
            myDB = new MyDB(context);
        }

        @Override
        public Object getItem(int i) {
            return null;
        }

        @Override
        public long getItemId(int i) {
            return 0;
        }

        @Override
        public View getView(int i, View view, ViewGroup viewGroup) {

            if (view == null) {
                view = View.inflate(context, R.layout.waiting_orders_line, null);
            }

            TextView name = (TextView) (view.findViewById(R.id.textView7));
            TextView membersNum = (TextView) (view.findViewById(R.id.textView10));
            TextView tableNum = (TextView) (view.findViewById(R.id.textView9));
            TextView price = (TextView) (view.findViewById(R.id.textView8));
            CheckBox isServed = (CheckBox) (view.findViewById(R.id.checkBox2));

            Orders orders = OrderstList.get(i);
            name.setText(orders.getName());
            membersNum.setText(orders.getNumOfpeople() + "");
            tableNum.setText(orders.getTableId() + "");
            price.setText((orders.getTotalPrice()) + "");
            isServed.setChecked(false);
            isServed.setOnCheckedChangeListener(listener);
            isServed.setTag(orders);
            return view;

        }

        CheckBox.OnCheckedChangeListener listener= new CheckBox.OnCheckedChangeListener() {
            @Override
            public void onCheckedChanged(CompoundButton compoundButton, boolean b) {
                int f=(b==true)?1:0;
                myDB.updateTable(((Orders)compoundButton.getTag()).getOrderId(), f);
                OrderstList.remove(((Orders)compoundButton.getTag()));
                myDB.updateIsServedOrder(((Orders)compoundButton.getTag()).getOrderId());
                notifyDataSetChanged();
            }
        };

}


