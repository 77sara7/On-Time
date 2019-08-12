package com.example.a301265963.myapplication;

import android.content.Context;
import android.graphics.drawable.Drawable;
import android.text.Editable;
import android.text.TextWatcher;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.NumberPicker;
import android.widget.TextView;

import com.example.a301265963.myapplication.db.Product;
import com.example.a301265963.myapplication.new_order.NewOrderActivity;

import java.util.List;


/**
 * Created by 315561779 on 20/03/2017.
 */


public class MyAdapter extends BaseAdapter implements NumberPicker.OnFocusChangeListener {

    List<Product> productList = null;
    Context context = null;
    EditText num = null;

    @Override
    public int getCount() {
        return productList.size();
    }

    public MyAdapter(List<Product> productList, Context context) {
        this.productList = productList;
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

        if (view == null) {
            view = View.inflate(context, R.layout.line_product, null);
        }
        TextView productName = (TextView) view.findViewById(R.id.name);
        num = (EditText) view.findViewById(R.id.num);
        TextView price = (TextView) view.findViewById(R.id.price);
        ImageView image = (ImageView) view.findViewById(R.id.pic);


        Product p = productList.get(i);
        productName.setText(p.getProductName() + "");
        price.setText(p.getProductPrice() + "");
        num.setTag(productList.get(i));

        image.setImageResource(p.getpImageId());
        num.addTextChangedListener(new TextWatcher() {
            @Override
            public void beforeTextChanged(CharSequence charSequence, int i, int i1, int i2) {

            }

            @Override
            public void onTextChanged(CharSequence charSequence, int i, int i1, int i2) {

                Integer numPortions = 0;
                if (!charSequence.toString().equals(""))
                    numPortions = Integer.parseInt(charSequence.toString());

                int price1 = productList.get(i).getProductPrice() * productList.get(i).getpamount();
                int price2 = productList.get(i).getProductPrice() * numPortions;
                ((NewOrderActivity) context).updatePrice(price1, price2);
//                productList.get(i).setpamount( Integer.parseInt (num.getText().toString()));
            }

           /* public void onValueChange(NumberPicker numberPicker, int i, int i1) {

                ProductInMenu p=list.get(Integer.parseInt(numberPicker.getTag().toString()));
                ((NewOrderActivity) context).changePrice(p.getAmount()*p.getProductPrice(),p.getProductPrice()*numberPicker.getValue());
                p.setAmount(numberPicker.getValue());
            }
*/

            @Override
            public void afterTextChanged(Editable editable) {

            }
        });
        return view;
    }


    public void onFocusChange(View view, boolean b) {
        if (b) {

         /* int index = Integer.parseInt(view.getTag().toString());
           productList.get(index).setAmount(Integer.parseInt(view.findViewById(R.id.numberPicker8).toString()));
           notifyDataSetChanged();*/
        }
    }
}
