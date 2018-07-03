<template>
    <div class="edit">
        <md-card class="md-layout-item md-small-size-100 md-medium-size-100 md-large-size-66 md-xlarge-size-50">
            <md-card-header>
                <div class="md-title">Edit {{getFriendlyName(tableName)}}</div>
            </md-card-header>

            <md-card-content>
                <md-field v-for="(column, index) in tableSchema.columns">
                    <label :for="column.name">{{getFriendlyName(column.name)}}</label>

                    <md-input :id="column.name"
                              :name="column.name"
                              :ref="column.name"
                              :type="getInputType(column.dataType)"
                              v-model="item[column.name]"
                              :maxlength="getMaxLength(column.maxLength)"
                              :disabled="column.isPrimaryKey"></md-input>
                </md-field>
            </md-card-content>

            <md-card-actions>
                <md-button v-on:click="save" class="md-raised md-accent">Save</md-button>
                <md-button v-on:click="cancel" class="md-raised">Cancel</md-button>
            </md-card-actions>
        </md-card>
    </div>
</template>

<script lang="ts" src="./Edit.ts"></script>
