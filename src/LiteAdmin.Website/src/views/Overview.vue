<template>
    <div class="overview">
        <md-table>
            <md-table-toolbar>
                <h1 class="md-title">{{getFriendlyName(tableName)}} Overview</h1>
            </md-table-toolbar>
            <md-table-row>
                <md-table-head v-for="(column, index) in tableSchema.columns"
                               :key="column.name"
                               v-if="index < 6 && !column.isPrimaryKey">
                    {{getFriendlyName(column.name)}}
                </md-table-head>
                <md-table-head></md-table-head>
            </md-table-row>
            <md-table-row v-for="item in items"
                          :key="item[tableKey]"
                          v-on:click="edit(item)">
                <md-table-cell v-for="(column, index) in tableSchema.columns"
                               :key="column.name"
                               v-if="index < 6 && !column.isPrimaryKey">
                    {{item[column.name]}}
                </md-table-cell>
                <md-table-cell class="buttons">
                    <md-button class="md-icon-button md-primary" v-on:click.stop="removeItem(item[tableKey])">
                        <md-icon class="">delete</md-icon>
                    </md-button>
                </md-table-cell>
            </md-table-row>
        </md-table>
        <md-button class="md-fab md-fab-bottom-right md-primary" :to="'/maintain/' + tableName + '/add'">
            <md-icon>add</md-icon>
        </md-button>

        <md-dialog-confirm :md-active.sync="showDialog"
                           md-title="Delete item?"
                           md-content="Are you sure you want to delete this item?"
                           md-confirm-text="Delete"
                           md-cancel-text="Cancel"
                           @md-cancel="cancel"
                           @md-confirm="confirm" />
    </div>
</template>

<script lang="ts" src="./Overview.ts"></script>
<style lang="scss" src="./Overview.scss"></style>
