package com.example.a301265963.project;

import android.content.Context;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.CompoundButton;
import android.widget.TextView;

import java.util.Date;
import java.util.List;

/**
 * Created by 318200318 on 19/04/2017.
 */

public class TablesAdapter extends BaseAdapter {

    int tag = 0;
    Context context;
    List<Table> TablesList;
    myDB mydb;
    ReserountMap reserountMap;

    public TablesAdapter(List<Table> list, ReserountMap c) {
        TablesList = list;
        reserountMap = c;
        mydb=new myDB(reserountMap);

    }

    public int getCount() {
        return TablesList.size();
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
    public View getView(int i, View curentView, ViewGroup viewGroup) {
        View newTable = curentView;
        if (newTable == null)
            newTable = View.inflate(reserountMap, R.layout.tableview, null);

        final Table t = (Table) TablesList.get(i);


        TextView tv = (TextView) newTable.findViewById(R.id.placesNumber);
        String places = " num of chairs: " + t.getPlaces();
        tv.setText((places));
        Button tvid = (Button) newTable.findViewById(R.id.tableId);
        tvid.setText("" + t.getId());
        tvid.setTag(i);
        tvid.setOnClickListener(chooselistener);
        CheckBox cb = (CheckBox) newTable.findViewById(R.id.isFull);
        boolean b = t.getIsFull();
        cb.setChecked(b);
        if (!b) {
            cb.setEnabled(false);

        }
        cb.setTag(i);
        cb.setOnCheckedChangeListener(listener);
        return newTable;
    }


    Button.OnClickListener chooselistener = new Button.OnClickListener() {
        @Override
        public void onClick(View view) {
            Table table = TablesList.get((Integer) view.getTag());
            reserountMap.choos(table.getId());
        }
    };
    CheckBox.OnCheckedChangeListener listener = new CheckBox.OnCheckedChangeListener() {
        @Override
        public void onCheckedChanged(CompoundButton compoundButton, boolean b) {
            Table table = TablesList.get((Integer) compoundButton.getTag());
            String bo = table.getIsFull() + "";
            table.setFull(b);
            compoundButton.setEnabled(false);
            mydb.updateIsFullTable(table.getId(), "false");
            Date date=new Date();
            mydb.updateEndOrder(table.getId(),date);
        }
    };
}
