package com.example.a301265963.project.orders;

import android.content.Context;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.Button;
import android.widget.TextView;

import com.example.a301265963.project.R;
import com.example.a301265963.project.myDB;

import java.util.List;

/**
 * Created by 301265963 on 22/03/2017.
 */


    public  class ExistingOrdersAdapter extends BaseAdapter {
    Context context;
       List<Order> Order;
    myDB mydb;
    public  ExistingOrdersAdapter(Context c, List<Order> e){
        Order=e;
        context=c;
        mydb=new myDB(c);
    }
        @Override
        public int getCount() {
            return Order.size();
        }

        @Override
        public Object getItem(int i) {
            return null;
        }

        @Override
        public long getItemId(int i) {
            return i;
        }

        @Override
        public View getView(int i, View view, ViewGroup viewGroup) {

            View newOrder = view;
            View newP = view;
            if (newOrder == null)
                newOrder = View.inflate(context, R.layout.existsorders, null);


            final Order p = (Order) Order.get(i);

            TextView tv = (TextView) newOrder.findViewById(R.id.existId);
            tv.setText(p.getOrderId()+"");

            Button button=(Button)newOrder.findViewById(R.id.leftButton);
button.setTag(i);
            button.setOnClickListener(chooselistener);
            return newOrder;
        }
        Button.OnClickListener chooselistener = new Button.OnClickListener() {
            @Override
            public void onClick(View view) {
               Order o = Order.get((Integer) view.getTag());
mydb.updateIsFullTable(o.getTableId(),"false");

            }
        };
    }

