package com.example.a301265963.project;

import android.content.Intent;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.EditText;
import android.widget.GridView;
import android.widget.TextView;
import android.widget.Toast;

import java.util.List;

public class ReserountMap extends AppCompatActivity {
    myDB myDB;
    GridView gridView;
    EditText editText;
//int TableId;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        myDB = new myDB(getApplicationContext());
        int degel = (int) getIntent().getIntExtra("degel", 0);
        setContentView(R.layout.activity_reserount_map);
        editText=(EditText)findViewById(R.id.ta);
     editText.setVisibility(View.GONE);
        gridView = (GridView) findViewById(R.id.gridView);
        List<Table> allTables = myDB.getAllTables();
        TablesAdapter T = new TablesAdapter(allTables, this);
        gridView.setAdapter(T);
        if(degel==3)
sendDataBack();
    }

    public void sendDataBack() {
        String table="";
editText.setVisibility(View.VISIBLE);
        int chairs = getIntent().getIntExtra("chairsNumber", 0);

        List<Table> tables = myDB.getAllTables();
        for (Table t : tables
                ) {
            if (t.getIsFull() != true && t.getPlaces() >= chairs)
             table+=" "+t.getId();


        }
        TextView textView=(TextView)findViewById(R.id.ta);
        textView.setText(table);
      if(table.isEmpty())
        Toast.makeText(this, "no chosen tables", Toast.LENGTH_LONG).show();

//          finish();
    }
public void choos(int TableId){
        Intent intent = new Intent();
        intent.putExtra("TableId",TableId);
    setResult(RESULT_OK, intent);}

}
