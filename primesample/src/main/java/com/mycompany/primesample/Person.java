package com.mycompany.primesample;

import java.io.Serializable;
import java.util.List;

public class Person implements Serializable {
    private static final long serialVersionUID = 1L;

    private int id;
    private String name;
    private List<Person> children;

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public List<Person> getChildren() {
        return children;
    }

    public void setChildren(List<Person> children) {
        this.children = children;
    }
}
