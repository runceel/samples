package com.mycompany.primesample.tree;

import java.util.List;
import org.primefaces.model.DefaultTreeNode;
import org.primefaces.model.TreeNode;

public class LazyTreeNode extends DefaultTreeNode {
    private static final long serialVersionUID = 1L;
    
    private boolean loaded;
    private LazyTreeNodeHandler handler;

    public LazyTreeNode() {
        super();
    }
    
    public LazyTreeNode(Object data, TreeNode parent) {
        super(data, parent);
    }

    public LazyTreeNode(String type, Object data, TreeNode parent) {
        super(type, data, parent);
    }
    
    public static interface LazyTreeNodeHandler {
        int getChildCount(Object parent);
        List getChildren(Object parent);
    }

    @Override
    public boolean isLeaf() {
        return this.getChildCount() == 0;
    }

    @Override
    public int getChildCount() {
        return this.handler.getChildCount(this.getData());
    }

    @Override
    public List<TreeNode> getChildren() {
        if (this.loaded) {
            return super.getChildren();
        }
        
        this.loaded = true;
        List l = this.handler.getChildren(this.getData());
        for (Object item : l) {
            LazyTreeNode child = new LazyTreeNode(item, this);
            child.setHandler(this.getHandler());
        }
        return super.getChildren();
    }

    public LazyTreeNodeHandler getHandler() {
        return handler;
    }

    public void setHandler(LazyTreeNodeHandler handler) {
        this.handler = handler;
    }
    
    public void reset() {
        this.loaded = false;
        super.getChildren().clear();
    }
}
