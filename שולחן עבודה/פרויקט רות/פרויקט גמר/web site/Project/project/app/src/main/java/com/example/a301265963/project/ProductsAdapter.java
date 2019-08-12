package com.example.a301265963.project;

import android.content.Context;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.ImageView;
import android.widget.NumberPicker;
import android.widget.TextView;

import java.util.List;

/**
 * Created by 318200318 on 23/04/2017.
 */

public class ProductsAdapter extends BaseAdapter {
    Context context;
    List<Product> ProductsList;
    newOrder neworder = null;

    public ProductsAdapter(newOrder c, List<Product> PL) {
        context = c;
        ProductsList = PL;
        this.neworder = c;

    }

    @Override
    public int getCount() {
        return ProductsList.size();
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

        View newProduct = view;
        View newP = view;
        if (newProduct == null)
            newProduct = View.inflate(context, R.layout.productview, null);


        final Product p = (Product) ProductsList.get(i);

        TextView tv = (TextView) newProduct.findViewById(R.id.pName);
        tv.setText(p.getName());
        TextView tv2 = (TextView) newProduct.findViewById(R.id.price);
        String price = p.getPrice() + "$";
        tv2.setText(price);
        NumberPicker np = (NumberPicker) newProduct.findViewById(R.id.numberP);
        np.setMinValue(0);
        np.setMaxValue(10);
        ImageView imageView = (ImageView) newProduct.findViewById(R.id.imageP);
        imageView.setImageResource(p.getImage());
        np.setTag(p);
        np.setOnValueChangedListener(listener);
        return newProduct;
    }

    NumberPicker.OnValueChangeListener listener = new NumberPicker.OnValueChangeListener() {
        int amount=0;
        @Override
        public void onValueChange(NumberPicker numberPicker, int i, int i1) {
            {

                Product p = (Product) numberPicker.getTag();
               if(p.getAmount()==0)
                amount=numberPicker.getValue();
               else {

                       amount = numberPicker.getValue() - amount;

               }
                p.setAmount(numberPicker.getValue());

                neworder.updateTotal(p.getPrice() * amount);
                amount=p.getAmount();

            }
        }
    };


}
