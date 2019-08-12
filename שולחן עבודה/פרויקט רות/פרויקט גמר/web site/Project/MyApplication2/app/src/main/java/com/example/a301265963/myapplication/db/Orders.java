package com.example.a301265963.myapplication.db;

/**
 * Created by 315561779 on 19/04/2017.
 */

public class Orders {
    public int getOrderId() {
        return orderId;
    }

    public void setOrderId(int orderId) {
        this.orderId = orderId;
    }

    public int isServed() {
        return isServed;
    }

    public void setServed(int served) {
        isServed = served;
    }

    public Orders(int orderId, String name, String startTime, String endTime, int tableId, int numOfpeople, int totalPrice, int isServed) {
        this.orderId = orderId;
        this.name = name;
        this.startTime = startTime;
        this.endTime = endTime;
        this.tableId = tableId;
        this.numOfpeople = numOfpeople;
        this.totalPrice = totalPrice;
        this.isServed = isServed;
    }

    public double getTotalPrice() {
        return totalPrice;
    }

    public void setTotalPrice(int totalPrice) {
        this.totalPrice = totalPrice;
    }

    public int getNumOfpeople() {
        return numOfpeople;
    }

    public void setNumOfpeople(int numOfpeople) {
        this.numOfpeople = numOfpeople;
    }

    public int getTableId() {
        return tableId;
    }

    public void setTableId(int tableId) {
        this.tableId = tableId;
    }

    public String getEndTime() {
        return endTime;
    }

    public void setEndTime(String endTime) {
        this.endTime = endTime;
    }

    public String getStartTime() {
        return startTime;
    }

    public void setStartTime(String startTime) {
        this.startTime = startTime;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    int orderId;
    String name;
    String startTime;
    String endTime;
    int tableId;
    int numOfpeople;
    int totalPrice;
    int isServed;


}
