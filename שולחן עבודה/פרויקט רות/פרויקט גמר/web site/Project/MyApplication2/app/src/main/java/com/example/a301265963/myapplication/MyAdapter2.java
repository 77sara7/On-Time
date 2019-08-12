package com.example.a301265963.myapplication;

/**
 * Created by 315561779 on 23/04/2017.
 */

import android.content.Context;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.CheckBox;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.NumberPicker;
import android.widget.TextView;

import com.example.a301265963.myapplication.db.Product;
import com.example.a301265963.myapplication.db.Table;

import java.util.List;

public class MyAdapter2 extends BaseAdapter implements NumberPicker.OnFocusChangeListener {

    List<Table> tablesList = null;
    Context context = null;

    @Override
    public int getCount() {
        return tablesList.size();
    }

    public MyAdapter2(List<Table> tablesList, Context context) {
        this.tablesList = tablesList;
        this.context = context;
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

        if (view == null)
            view = View.inflate(context, R.layout.line_table, null);


        TextView numPlaces = (TextView) view.findViewById(R.id.num_places);
        ImageView image1 = (ImageView) view.findViewById(R.id.panui_tafus);
        ImageView image = (ImageView) view.findViewById(R.id.imageTable);
        TextView idt = (TextView) view.findViewById(R.id.idT);

        Table t = tablesList.get(i);
        numPlaces.setText(t.getPlaces() + "");
        idt.setText(t.getId() + "");
        if (t.isFull() == 1)
            image1.setImageResource(R.drawable.x);
        else
            image1.setImageResource(R.drawable.v);

        return view;
    }

    @Override
    public void onFocusChange(View view, boolean b) {
        if (b) {
/*

           int index = Integer.parseInt(view.getTag().toString());
           tablesList.get(index).setFull(Integer.parseInt(view.findViewById(R.id.panui_tafus).toString()));
           notifyDataSetChanged();
*/

        }
    }
}
