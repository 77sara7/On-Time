package com.example.a301265963.project.orders;

import android.content.Context;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.CheckBox;
import android.widget.CompoundButton;
import android.widget.TextView;

import com.example.a301265963.project.myDB;
import com.example.a301265963.project.R;

import java.util.List;

/**
 * Created by 301265963 on 22/03/2017.
 */

    /**
     * Created by 301265963 on 22/03/2017.
     */

    public class WaitingOrdersAdapter extends BaseAdapter {
        List<Order> WaitingOrders;
        Context context;
        myDB myDB;
        public WaitingOrdersAdapter(Context c,List<Order> orders){
            WaitingOrders=orders;
            context=c;
            myDB=new myDB(c);
        }

        @Override
        public int getCount() {
            return WaitingOrders.size();
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

            if (newOrder == null)
                newOrder = View.inflate(context, R.layout.waitingorders, null);


            final Order p = (Order) WaitingOrders.get(i);

            TextView tv = (TextView) newOrder.findViewById(R.id.orderid);
            tv.setText(p.getOrderId()+"");

            CheckBox checkBox=(CheckBox) newOrder.findViewById((R.id.cbserved));
    checkBox.setOnCheckedChangeListener(listener);
checkBox.setTag(i);
            return newOrder;
        }

        CheckBox.OnCheckedChangeListener listener = new CheckBox.OnCheckedChangeListener() {
            @Override
            public void onCheckedChanged(CompoundButton compoundButton, boolean b) {
                Order o = WaitingOrders.get((Integer) compoundButton.getTag());

                String bo = o.getServed()+"";
                o.setServed(b);
                compoundButton.setEnabled(false);
                myDB.updateServe(o.getOrderId());
            }
        };
    }

