package com.mycompany.primesample;

import com.mycompany.primesample.tree.LazyTreeNode;
import java.io.Serializable;
import java.util.ArrayList;
import java.util.List;
import java.util.Random;
import javax.annotation.PostConstruct;
import javax.faces.bean.ManagedBean;
import javax.faces.bean.ViewScoped;
import org.primefaces.model.DefaultTreeNode;

@ManagedBean
@ViewScoped
public class LazyTreePage implements Serializable {
    private DefaultTreeNode root;

    @PostConstruct
    public void init() {
        root = new DefaultTreeNode();
        LazyTreeNode.LazyTreeNodeHandler h = new LazyTreeNode.LazyTreeNodeHandler() {
            private final Random R = new Random();
            @Override
            public int getChildCount(Object parent) {
                System.out.println(((Person)parent).getId() + "#getChildCount called.");
                return 10;
            }
            @Override
            public List getChildren(Object parent) {
                System.out.println(((Person)parent).getId() + "#getChildren called.");
                Person p = (Person) parent;
                p.setChildren(new ArrayList<Person>());
                for (int i = 0; i < 10; i++) {
                    Person child = new Person();
                    child.setId(p.getId() + R.nextInt());
                    child.setName(p.getName() + "の子供");
                    p.getChildren().add(child);
                }
                return p.getChildren();
            }
        };
        Person p = new Person();
        p.setId(100);
        p.setName("田中　太郎");
        LazyTreeNode n1 = new LazyTreeNode(p, root);
        n1.setHandler(h);
    }
    
    public DefaultTreeNode getRoot() {
        return root;
    }

    public void setRoot(DefaultTreeNode root) {
        this.root = root;
    }
}
