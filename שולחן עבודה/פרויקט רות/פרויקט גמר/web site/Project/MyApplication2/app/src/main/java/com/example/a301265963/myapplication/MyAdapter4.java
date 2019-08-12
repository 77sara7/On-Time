package com.example.a301265963.myapplication;

import android.content.Context;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.CompoundButton;
import android.widget.EditText;
import android.widget.NumberPicker;
import android.widget.TextView;

import com.example.a301265963.myapplication.db.MyDB;
import com.example.a301265963.myapplication.db.Orders;

import java.util.Date;
import java.util.List;

/**
 * Created by 315561779 on 26/04/2017.
 */

public class MyAdapter4 extends BaseAdapter{

        List<Orders> OrderstList = null;
        Context context = null;
        EditText num=null;
        MyDB myDB=null;

        @Override
        public int getCount() {
            return OrderstList.size();
        }

        public MyAdapter4(List<Orders> OrderstList, Context context) {
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
                view = View.inflate(context, R.layout.attending_order_line, null);
            }

            TextView name = (TextView) (view.findViewById(R.id.textView12));
            TextView membersNum = (TextView) (view.findViewById(R.id.textView13));
            TextView tableNum = (TextView) (view.findViewById(R.id.textView14));
            TextView price = (TextView) (view.findViewById(R.id.textView15));
            Button button = (Button) (view.findViewById(R.id.button30));

            button.setTag(OrderstList.get(i));
            final Orders orders = OrderstList.get(i);
            name.setText(orders.getName());
            membersNum.setText(orders.getNumOfpeople() + "");
            tableNum.setText(orders.getTableId() + "");
            price.setText((orders.getTotalPrice()) + "");
            button.setOnClickListener(new  View.OnClickListener() {
                @Override
                public void onClick(View view) {
                    myDB.updateTable(orders.getTableId(),0);
                    myDB.updateEndTimeOrder(orders.getOrderId(),String.valueOf(new Date()));
                    OrderstList.remove(view.getTag());
                    notifyDataSetChanged();
                }
            });
            return view;

        }

    }


