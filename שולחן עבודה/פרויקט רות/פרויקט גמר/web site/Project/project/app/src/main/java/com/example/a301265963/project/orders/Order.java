package com.example.a301265963.project.orders;

/**
 * Created by 318200318 on 24/04/2017.
 */

public class Order {
     public int totalPrice;
public  int OrderId;public int TableId;
 public int peopleNumber;
public String name;

    public int getPeopleNumber() {
        return peopleNumber;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public int getTableId() {
        return TableId;
    }

    public void setTableId(int tableId) {
        TableId = tableId;
    }

    public Order(int i, String _isServed,int TI,String _name,int people,int t){
        OrderId=i;
  isServed=Boolean.parseBoolean(_isServed);
    TableId=TI;
    name=_name;
    peopleNumber=people;
    totalPrice=t;}
    public Boolean getServed() {
        return isServed;
    }

    public void setServed(Boolean served) {
        isServed = served;
    }

    public int getTotalPrice() {
        return totalPrice;
    }

    public void setTotalPrice(int totalPrice) {
        this.totalPrice = totalPrice;
    }

    public int getOrderId() {
        return OrderId;
    }

    public void setOrderId(int orderId) {
        OrderId = orderId;
    }

    Boolean isServed;
}
