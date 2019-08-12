package com.example.a301265963.myapplication.table;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.support.v7.view.menu.ListMenuPresenter;
import android.view.View;
import android.widget.AdapterView;
import android.widget.GridView;
import android.widget.ImageView;
import android.widget.NumberPicker;
import android.widget.TextView;
import android.widget.Toast;

import com.example.a301265963.myapplication.MyAdapter2;
import com.example.a301265963.myapplication.R;
import com.example.a301265963.myapplication.db.MyDB;
import com.example.a301265963.myapplication.db.Table;

import java.util.Date;
import java.util.List;


public class TablesActivity extends AppCompatActivity {

    //@Override
    List<Table> ls = null;
    TextView textView1=null;
    NumberPicker numberPicker =null;

    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_t);
        final MyDB myDB = new MyDB(getApplicationContext());
        myDB.addTable(1, 5, 1);
        myDB.addTable(2, 2, 0);
        myDB.addTable(3, 10, 0);
        myDB.addTable(4, 6, 1);
        myDB.addTable(5, 4, 0);
        myDB.addTable(6, 4, 0);
        myDB.addTable(7, 5, 1);

        ls = myDB.getTables();
        textView1= (TextView) findViewById(R.id.errors);
        MyAdapter2 myAdapter2 = new MyAdapter2(ls, this);
        final GridView gridView = (GridView) findViewById(R.id.gvt);
        gridView.setAdapter(myAdapter2);
        TextView textView = (TextView) findViewById(R.id.rellevantT);

        numberPicker = (NumberPicker) findViewById(R.id.numberPicker);
        if (getIntent().getStringExtra("from").equals("order")) {
            textView.setText(R.string.availableT);
            for(int i=0;i<ls.size();i++){
                if(ls.get(i).isFull()==0 && getIntent().getIntExtra("num",1) <= ls.get(i).getPlaces()){
                    textView.setText(textView.getText()+" "+ls.get(i).getId());
                }
            }


        }
        gridView.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> adapterView, View view, int i, long l) {
                ImageView image = (ImageView) view.findViewById(R.id.panui_tafus);
                if (getIntent().getStringExtra("from").equals("main")) {
                    if (ls.get(i).isFull() == 1) {
                        ls.get(i).setFull(0);
                        myDB.updateTable(ls.get(i).getId(),0);
                        image.setImageResource(R.drawable.v);
                        myDB.updateEndTimeOrder(ls.get(i).getId(), String.valueOf(new Date()));
                    }
                } else if (ls.get(i).isFull() == 0) {
                   /*if(((int) numberPicker.getValue())>ls.get(i).getPlaces())
                        textView1.setText(R.string.msgNumPlaces);
                    else {*/
                    if(getIntent().getIntExtra("num",1)>ls.get(i).getPlaces())
                        textView1.setText(R.string.msgNumPlaces);
                    else {
                        ls.get(i).setFull(1);
                        Intent intent = new Intent();
                        intent.putExtra("tableId", ls.get(i).getId());
                        setResult(RESULT_OK, intent);
                        finish();
                    }
                } else{
                    textView1.setText(R.string.msg);
                }
            }
        });
    }
}


