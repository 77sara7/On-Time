package com.example.a301265963.project.orders;

import android.content.Context;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.TextView;

//import com.example.a301265963.project.R;

import com.example.a301265963.project.R;

import java.util.List;

/**
 * Created by 301265963 on 22/03/2017.
 */


    /**
     * Created by 301265963 on 22/03/2017.
     */

    public class AllOrdersAdapter extends BaseAdapter {
        List<Order> AllOrders;
        Context c;
        public AllOrdersAdapter(Context context,List<Order> orders){
            AllOrders=orders;
            c=context;
        }

        @Override
        public int getCount() {
            return AllOrders.size();
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

            View newOrder;
            if (view == null)
                newOrder = View.inflate(c, R.layout.allorders, null);
            else
            newOrder=view;

            final Order p = (Order) AllOrders.get(i);

            TextView tv = (TextView) newOrder.findViewById(R.id.customerN);
            tv.setText(p.getName());
            TextView tv2 = (TextView) newOrder.findViewById(R.id.customerP);
            String price = p.totalPrice + "$";
            tv2.setText(price);
            TextView tv3 = (TextView) newOrder.findViewById(R.id.customerT);
            String TableId = p.getTableId() + "";
            tv3.setText(TableId);
            TextView tv4 = (TextView) newOrder.findViewById(R.id.customerNumber);
            String people = p.getPeopleNumber() + "";
            tv4.setText(people);
            return newOrder;
        }
    }

